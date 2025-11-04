using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace TT425_Lotus_Monorail
{
    public partial class mainWindow : Form
    {
        /*===GLOBAL VARIABLES===*/
        //####
        String defaultDirectory = @"C:\sawfiles_test"; //default directory for .prt files, change to C:\sawfiles_two when live
        string currentSelectedUSBDrive = ""; //global that holds the current USB drive letter/path, starts blank
        int currentNumberOfPrtFilesDetectedInFolder; //Global that holds the current folder selections number of PRT files.
        string[] prtFilesToBeWritten = new string[300]; //Array storing the names of files to be written
        int numberOfFiles2BeWrittenGlobal = 0; //global storage for the number of files ready to be written to usb
        //####

        /*===BOOLEANS AND ERROR CHECKERS===*/
        //#
        Boolean removableDrivesAvailable = false; //Becomes true if USB/Removable Drives available
        Boolean sawFolderExists = false; //Becomes true if the default directory exists.
        Boolean sawFolderFilesArePRT = false;   //Becomes true if there are .prt files in the saw files folder
        Boolean USBComparisonSuccessful = false; //becomes true if USB has been checked and there are files ready to write
        Boolean powerReady = false; //Becomes true if it is okay to begin writing files
        Boolean sawFileNotYetWritten = false;   //Becomes true if the saw file does not also exist on target removable drive
        Boolean extremeErrorState = false; //Becomes true if an exception is thrown that should stop the program from continuing
        Boolean prtOnUSB = false; //becomes true if there are .prt files on the usb drive
        //#


        public mainWindow()
        //###
        //Main Window and Startup process
        {
            //Initialization/Runtime Commands
            InitializeComponent();
            //Startup Processes
            printToLog("loaded!");
            folderLocationBox.Text = defaultDirectory;

            //Drive Check
            removableDrivesSelection.Items.Add("No USB Drives Found");
            checkRemovableDrives();

            //Try to create default directory (C:\sawfiles_two) if doesnt exist
            checkIfDefaultDirectoryExists();

            //Check default location for files
            if (sawFolderExists)
            //Continue if default folder exists, which it should even if it has to be created.
            {
                checkForPRTFiles(defaultDirectory); //Check default directory for PRT Files
                CheckUSBProcess();
            }


        }
        //###

        //Folder Picker Methods//
        private void locationBoxClick(object sender, MouseEventArgs e)
        {
            changeFolderLocation();
        }
        private void changeFolderIcon_Click(object sender, EventArgs e)
        {
            changeFolderLocation();
        }
        //--------------------//

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            GoLabel.Visible = false;
            PowerButton.Image = Properties.Resources.PowerIdle;
            checkRemovableDrives();
        }

        private void removableDrivesSelection_SelectedIndexChanged(object sender, EventArgs e)
        //If dropdown changed, only perform a check if there's removable drives accessible
        {
            GoLabel.Visible = false;
            PowerButton.Image = Properties.Resources.PowerIdle;
            CheckUSBProcess();
        }
        private void PowerButton_Click(object sender, EventArgs e)
        {
            if (powerReady)
            {
                convertFilesToUSB();
            }
        }

        private void changeFolderLocation()
        {
            GoLabel.Visible = false;
            PowerButton.Image = Properties.Resources.PowerIdle;
            //Method for picking a file, cut and paste to whichever button you
            //end up using to select this...
            DialogResult location2StartResult = folderSelector.ShowDialog();
            if (location2StartResult == DialogResult.OK)
            {

                string chosenStartFolder = folderSelector.SelectedPath;
                folderLocationBox.Text = chosenStartFolder;
                checkForPRTFiles(chosenStartFolder);
                CheckUSBProcess();
            }
        }

        private void CheckUSBProcess()
        {
            if (removableDrivesAvailable && sawFolderFilesArePRT)
            {
                string justTheDriveLetterPlease = ""; //initialise string
                string? selectedDriveFullName = ""; //initialise non null string
                string chosenStartFolder = folderSelector.SelectedPath;
                //Method here to grab Drive Letter From Selection
                selectedDriveFullName = removableDrivesSelection.GetItemText(removableDrivesSelection.SelectedItem);
                if (selectedDriveFullName != null)
                { //Makes sure there's actual information in the name, and isn't null
                    justTheDriveLetterPlease = selectedDriveFullName.Substring(0, 3);
                }
                string USBDriveToChangeTo = justTheDriveLetterPlease;
                currentSelectedUSBDrive = USBDriveToChangeTo;

                //Comparison process jump
                compareFolderFilesToUSB(chosenStartFolder, USBDriveToChangeTo, currentNumberOfPrtFilesDetectedInFolder);

            }
            else if (removableDrivesAvailable && !sawFolderFilesArePRT)
            //If USB detected but no .prt files in folder
            {
                USBStatusText.Text = $"No .prt files to write";
                USBCheckLight.Image = Properties.Resources.FileCheckNG;
            }
            else
            //If removable drives not available
            {
                USBStatusText.Text = $"No USB";
                USBCheckLight.Image = Properties.Resources.FileCheckNG;
                //CHANGE THIS to code to reset the PRT comparison box
            }
        }

        private void printToLog(string text2Print)
        //Appends log text to the top of the log box
        {


            DateTime currentDate = DateTime.Now;
            String justTheTime = currentDate.ToString("HH:mm:ss");
            //Check Size of log, will wipe when very very long
            if (logBox.Text.Length > 200000)
            {
                logBox.Text = ("[" + justTheTime + "]" + " Log cleared");
            }
            logBox.Text = ("[" + justTheTime + "] " + text2Print + Environment.NewLine + logBox.Text);
        }

        private void checkRemovableDrives()
        //Checks if there is available USB or Removable disks
        {
            removableDrivesSelection.Items.Clear();
            removableDrivesAvailable = false; //RESET FLAG
            int driveIndex = 0;
            printToLog("Checking for Drives...");
            try
            {
                DriveInfo[] allSystemDrives = DriveInfo.GetDrives();
                foreach (DriveInfo currentItem in allSystemDrives)
                {

                    if (currentItem.DriveType == DriveType.Removable)
                    {
                        driveIndex++; //Count new drive
                        removableDrivesAvailable = true;
                        //Calculate size//
                        decimal driveSizeInBytes = currentItem.TotalSize;
                        String driveSizeFinalString = "";
                        if (driveSizeInBytes < 1000000000)
                        {
                            //Under 1GB so convert to a MB reading
                            driveSizeInBytes = (driveSizeInBytes / 1000000); //MB Value
                            driveSizeInBytes = Math.Round(driveSizeInBytes, 0);
                            driveSizeFinalString = (driveSizeInBytes + "MB");
                        }
                        else if (driveSizeInBytes < 1000000)
                        {
                            //Under 1MB so convert to a KB reading
                            driveSizeInBytes = (driveSizeInBytes / 1000); //MB Value
                            driveSizeInBytes = Math.Round(driveSizeInBytes, 0);
                            driveSizeFinalString = (driveSizeInBytes + "KB");
                        }
                        else
                        {
                            //Over 1GB so convert to GB reading
                            driveSizeInBytes = (driveSizeInBytes / 1000000000); //GB value
                            driveSizeInBytes = Math.Round(driveSizeInBytes, 2);
                            driveSizeFinalString = (driveSizeInBytes + "GB");
                        }
                        //Size Caluclation Complete
                        //Volume Label//
                        String volumeLabelName = currentItem.VolumeLabel;
                        //Check for blank name
                        if (volumeLabelName == "")
                        {
                            volumeLabelName = "Unnamed Drive";
                        }

                        String foundDriveInfo = (currentItem.Name + " " +
                                                "[" + volumeLabelName + "] " + driveSizeFinalString);
                        printToLog("Found Drive: " + foundDriveInfo);
                        removableDrivesSelection.Items.Add(foundDriveInfo);
                        if (driveIndex == 1)
                        {
                            //If first USB found, set as default path, as this will also be the dropdown default selection.
                            currentSelectedUSBDrive = currentItem.Name;
                            printToLog($"Setting default USB path as {currentSelectedUSBDrive}");
                        }
                    }

                }
                //Change dropdown Default to first USB
            }//END OF TRY LOOP
            catch (Exception ex)
            //If there was a IO error with checking drives
            {
                printToLog("Drive Check Error: " + ex.Message);
                removableDrivesAvailable = false;
            }
            //END OF DRIVE CHECK LOOP
            if ((removableDrivesAvailable == false) | (removableDrivesSelection.Items.Count == 0))
            //If no removable drives found
            {
                printToLog("No USB Drives found!");
                removableDrivesSelection.Items.Add("No USB Drives Found");

            }
            if (removableDrivesAvailable == true)
            {
                removableDrivesSelection.SelectedIndex = 0;
            }
            else
            {
                removableDrivesSelection.SelectedIndex = removableDrivesSelection.Items.Count - 1;
            }
            printToLog("Drive Check Complete");
            //FLAG RESET
        }

        private void checkIfDefaultDirectoryExists()
        //Checks for the existence of C:\sawfiles_two, creates location if not
        {
            printToLog("Checking if " + defaultDirectory + " exists...");
            Boolean doesDefaultExist = System.IO.Directory.Exists(defaultDirectory);
            if (doesDefaultExist)
            {
                printToLog($"{defaultDirectory} found");
                sawFolderExists = true;
            }
            else
            {
                printToLog($"{defaultDirectory} not found");
                printToLog($"Creating {defaultDirectory}");
                try
                {
                    System.IO.Directory.CreateDirectory(defaultDirectory);
                    sawFolderExists = true;
                }
                catch (Exception ex)
                {
                    printToLog($"Error creating {defaultDirectory}: " + ex.Message);
                    fileCheckerStatusText.Text = ($"Error creating {defaultDirectory}");
                    sawFolderExists = false;
                }
            }
        }

        private void checkForPRTFiles(String directoryToScan)
        {
            GoLabel.Visible = false;
            PowerButton.Image = Properties.Resources.PowerIdle;
            printToLog($"Checking {directoryToScan} for .prt files...");
            string[] fileNames = Directory.GetFiles(directoryToScan, "*.prt", SearchOption.TopDirectoryOnly);
            if (fileNames.Length != 0)
            //Files Found
            {
                int numberOfPRTFilesFound = 0;
                foreach (string fileName in fileNames)
                {
                    numberOfPRTFilesFound++;
                    printToLog($"Found {fileName}");
                }
                statusLight.Image = Properties.Resources.FileCheckOk;
                currentNumberOfPrtFilesDetectedInFolder = numberOfPRTFilesFound;
                fileCheckerStatusText.Text = $"Found {numberOfPRTFilesFound} .prt files in folder";
                sawFolderFilesArePRT = true;
            }
            else
            //File Not Found
            {
                sawFolderFilesArePRT = false;
                fileCheckerStatusText.Text = $"No .prt files found in folder";
                statusLight.Image = Properties.Resources.FileCheckNG;
                printToLog($"No .prt files found in {directoryToScan}");
                sawFolderFilesArePRT = false;

            }
        }

        private void compareFolderFilesToUSB(string locationToCheck, string USBToCheck, int numberOfPrtFilesInFolder)
        //This method is only reached when a USB is available and .prt files are found in the chosen folder
        //So, don't worry about not having access to drive names or folder paths - They are already checked
        {
            USBComparisonSuccessful = false; //RESET FLAG
            printToLog($"Comparing contents of {locationToCheck} to USB Drive {USBToCheck}");

            //First, create a string array of .prt files in the original directory
            string[] fileNames = Directory.GetFiles(locationToCheck, "*.prt", SearchOption.TopDirectoryOnly);
            string[] listOfPrtFilesInFolder = new string[numberOfPrtFilesInFolder];
            string[] listOfPrtFilesTruncated = new string[numberOfPrtFilesInFolder];
            int numberOfPrtFilesOnUsb = 0; //Reset Count
            int numberOfUSBFilesAlreadyWritten = 0; //Reset, but this also counts how many files don't need writing
            if (fileNames.Length != 0)
            //.prt detected, add to list
            {
                //Generate list of prt files in folder
                for (int fileIndex = 0; fileIndex < numberOfPrtFilesInFolder; fileIndex++)
                {
                    listOfPrtFilesInFolder[fileIndex] = fileNames[fileIndex];
                }

                //Truncate list so that only filename and .prt remain
                for (int fileIndex = 0; fileIndex < numberOfPrtFilesInFolder; fileIndex++)
                {
                    string locationToCheckWithPathSetProperly = locationToCheck + "\\";
                    listOfPrtFilesTruncated[fileIndex] = listOfPrtFilesInFolder[fileIndex].Replace(locationToCheckWithPathSetProperly, "");
                }

            }
            //NOW, check USB for .prts, change checklight to purple with status "USB and Folder In Sync" if list of .prts is the same as array in folder.
            string[] USBFiles = Directory.GetFiles(USBToCheck, "*.prt", SearchOption.TopDirectoryOnly);

            if (USBFiles.Length != 0)
            //Files Found
            {
                foreach (string USBFileName in USBFiles)
                {
                    //Count number directly
                    numberOfPrtFilesOnUsb++;
                }
            }
            if (numberOfPrtFilesOnUsb > 0)
            //CONTINUE IF PRT FILES FOUND ON USB
            {
                string[] listOfUSBPrtFiles = new string[numberOfPrtFilesOnUsb];
                string[] listOfUSBPrtFilesTruncated = new string[numberOfPrtFilesOnUsb];
                for (int fileIndex = 0; fileIndex < numberOfPrtFilesOnUsb; fileIndex++)
                {
                    //Add to array of names
                    listOfUSBPrtFiles[fileIndex] = USBFiles[fileIndex];
                }

                //truncate list to just filenames
                for (int fileIndex = 0; fileIndex < numberOfPrtFilesOnUsb; fileIndex++)
                {
                    string locationOnUSBSet = USBToCheck;
                    listOfUSBPrtFilesTruncated[fileIndex] = listOfUSBPrtFiles[fileIndex].Replace(locationOnUSBSet, "");
                }

                //NOW, Compare the lists and create a new array of files that only exist on the sawFolder
                int numberOfFiles2BeWritten = 0;
                foreach (string SawFolderFile2Check in listOfPrtFilesTruncated) //loop through every file found in folder
                {
                    Boolean doesFileAlreadyExistOnUsb = false;

                    foreach (string USBFile2CheckAgainst in listOfUSBPrtFilesTruncated) //Check against every USB file
                    {
                        if (SawFolderFile2Check == USBFile2CheckAgainst) //if sawFolder file being checked is the same as a usb file
                        {
                            doesFileAlreadyExistOnUsb = true;

                        }
                    }


                    if (!doesFileAlreadyExistOnUsb) //if file is not on USB, add it to the array of files to be written
                    {
                        prtFilesToBeWritten[numberOfFiles2BeWritten] = SawFolderFile2Check;
                        printToLog($"{prtFilesToBeWritten[numberOfFiles2BeWritten]} Not Found On USB!");
                        numberOfFiles2BeWritten++;

                    }


                }
                numberOfFiles2BeWrittenGlobal = numberOfFiles2BeWritten;
                if (numberOfFiles2BeWritten == 0)
                {
                    USBStatusText.Text = $"PC and USB Files are in sync";
                    printToLog($"Contents of {locationToCheck} are the same as {USBToCheck}");
                    USBCheckLight.Image = Properties.Resources.FileCheckSync;
                    GoLabel.Visible = false;
                    PowerButton.Image = Properties.Resources.PowerIdle;
                    powerReady = false;
                }
                if (numberOfFiles2BeWritten > 0)
                {
                    USBComparisonSuccessful = true;
                }

                //---------------------------------------------------------------------------------------//
            }
            else //IF NO USB FILES ARE FOUND ON THE USB
            {
                //All of the prt files should be truncated and added to the array
                for (int fileWriterIndex = 0; fileWriterIndex < listOfPrtFilesTruncated.Length; fileWriterIndex++)
                {
                    prtFilesToBeWritten[fileWriterIndex] = listOfPrtFilesTruncated[fileWriterIndex];
                }
                printToLog($"No .prt files found on USB");
                USBComparisonSuccessful = true;
                numberOfFiles2BeWrittenGlobal = currentNumberOfPrtFilesDetectedInFolder;

            }

            if (USBComparisonSuccessful)
            {
                USBStatusText.Text = ($"Ready to write {numberOfFiles2BeWrittenGlobal} .prt files");
                //SUCCESS STATE - WRAP WITH SUCCESS FLAG AFTER FINISHING
                USBCheckLight.Image = Properties.Resources.FileCheckOk;

                //WHEN RUN BUTTON ADDED, THIS CHANGES THE IMAGE TO AVAILABLE SO IT CAN BE CLICKED
                powerReady = true;
                GoLabel.Visible = true;
                PowerButton.Image = Properties.Resources.PowerReady;

                //-----------------------------------------------------------------------------------//
            }
        }

        private void convertFilesToUSB()
        //Files are now picked, convert all to modern .prt
        {
            PowerButton.Image = Properties.Resources.PowerActive;
            printToLog($"Writing {numberOfFiles2BeWrittenGlobal} .prt files to {currentSelectedUSBDrive}");
            for (int writerIndex = 0; writerIndex < numberOfFiles2BeWrittenGlobal; writerIndex++)
            //Loop that goes through each file to be printed and sends it to the write function
            {
                string pathOfFile2BeWritten = (folderLocationBox.Text + "\\" + prtFilesToBeWritten[writerIndex]);
                string fileName2print = prtFilesToBeWritten[writerIndex];
                printToLog($"Writing file {fileName2print} to {currentSelectedUSBDrive}");
                prtTo425CSV(pathOfFile2BeWritten, currentSelectedUSBDrive, (writerIndex));
            }


            //End of Write
            PowerButton.Image = Properties.Resources.PowerIdle;
        }

        private void prtTo425CSV(string pathOfPCFile, string targetUSB, int fileNumber)
        {
            //-------DEFAULTS AND INITIALISATION--------//
            int selectedLineInPRTFile = 0;
            int cuttingNo = fileNumber + 1;
            string cuttingLength = "000h"; //Size: ___ (can be _h or _w)
            string profileCode = "LANXXX"; //E.G. LAN106W, found on line 7 after AB^FD
            string leftHead = "45 | 90"; //based on |-| style drawing, run method to return number based on character, found just before "Right"
            String rightHead = leftHead; //These are copies of each other
            const int amount = 1; //always 1
            double height = 70.00; //Can query db to find out, but just leave at 70 for most part
            string orderNumber = "J0024569"; //Taken from Job No, after No: and before ^FS (do a double split)
            int poz = 1; //Not sure if matters, leave at 1.
            int assembly = (fileNumber + 1); //same as cuttingNo, changes with file, but shouldnt matter too much.
            //Ignore Trolley, Box axis, Gasket, Operation//
            int dealer = 1234567; //Barcode


            //------------------------------------------//
            if (File.Exists(pathOfPCFile))
            {
                string[] contentsOfPrt = File.ReadAllLines(pathOfPCFile); //read each line into array called contentsOfPrt
                foreach (string line in contentsOfPrt)
                {
                    //debug print
                    //printToLog($"{line}");
                }
                //------------PART 1------------//
                //---------cutting Number-------//
                printToLog($"Cutting Number: {cuttingNo}");
                //------------------------------//

                //------------PART 1------------//
                //------Get cutting length------//
                var prtLine8Array = contentsOfPrt[7].Split(' ');
                cuttingLength = prtLine8Array[1];
                if((cuttingLength.Substring(0, 1) == @"|") || (cuttingLength.Substring(0, 1) == @"-") || 
                    (cuttingLength.Substring(0, 1) == @"\") || (cuttingLength.Substring(0, 1) == @"/"))
                {
                    cuttingLength = "0"; //If cutting length is missing, hence the angle would be loaded instead
                }
                printToLog($"Cutting Length: {cuttingLength}");
                //------------------------------//



            } //End of loop that checks if file exists (safety loop)
            else
            {
                printToLog($"File error? Can't find {pathOfPCFile}");
                printToLog($"Please refresh USB drives!");
            }
        }


        //NOTES:
        /*
         * When files are converted and written to the USB, make sure you run every method for checking folder and USB to rescan all values. 
         * If everything was correct, then in theory the finished light will be the sync light as usbCheck, folder check etc will run and 
         * determine that the contents of the folder is exactly the same as the USB.
         * 
         */

        //POST-BETA UPGRADES:
        /*
         * Of course, basic USB functionality is all that's needed to push version 1, but after that I want to add many features, these include:
         * !! Saved settings for defaults, networking location etc
         * !! Network writing over ethernet
         * !! Daemon-like functionality, click run and it will automatically keep moving files from the location to target area
         * !! UI redesign
         */




        //--------------------------GENERAL UI, ANIMATIONS, HOVER CHANGES ETC-------------------------//
        private void RefreshButton_MouseEnter(object sender, EventArgs e)
        {
            RefreshButton.Image = Properties.Resources.RefreshUsbIconHover;
        }
        private void RefreshButton_MouseLeave(object sender, EventArgs e)
        {
            RefreshButton.Image = Properties.Resources.RefreshUsbIcon;
        }
        private void mainWindow_Click(object sender, EventArgs e)
        {
        }
        private void changeFolderIcon_MouseEnter(object sender, EventArgs e)
        {
            changeFolderIcon.Image = Properties.Resources.ChangeFolderIconHover;
        }
        private void changeFolderIcon_MouseLeave(object sender, EventArgs e)
        {
            changeFolderIcon.Image = Properties.Resources.ChangeFolderIcon;
        }

       

        private void PowerButton_MouseEnter(object sender, EventArgs e)
        {
            if (powerReady)
            {
                PowerButton.Image = Properties.Resources.PowerReadyHover;
            }
        }

        private void PowerButton_MouseLeave(object sender, EventArgs e)
        {
            if (powerReady)
            {
                PowerButton.Image = Properties.Resources.PowerReady;
            }
        }
        //--------------------------------------------------------------------------------------------//
    }
}
