using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace TT425_Lotus_Monorail
{
    public partial class mainWindow : Form
    {
        /*===GLOBAL VARIABLES===*/
        //####
        String defaultDirectory = @"C:\sawfiles_two"; //default directory for .prt files, change to C:\sawfiles_two when live
        string currentSelectedUSBDrive = ""; //global that holds the current USB drive letter/path, starts blank
        int currentNumberOfPrtFilesDetectedInFolder; //Global that holds the current folder selections number of PRT files.
        string[] prtFilesToBeWritten = new string[10000]; //Array storing the names of files to be written
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
        Boolean startupFolderCheck = true; //Starts true, turns false to indicate startup has ended and default folder should not be used
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
            folderSelector.SelectedPath = defaultDirectory;

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
            startupFolderCheck = false;

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
            checkForPRTFiles(folderLocationBox.Text);
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
                Point whereIsTheParentWindow = new Point();
                whereIsTheParentWindow = this.Location;
                loadingScreen pleaseWait = new loadingScreen(whereIsTheParentWindow);
                pleaseWait.Show();
                convertFilesToUSB();
                pleaseWait.Close();
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
                string chosenStartFolder = folderSelector.SelectedPath;  //
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
        /*
         * UPDATE: have since found out that all .prt files end in _1 if alone, so there is no point in checking for a blank
         * file end. Therefore, all of this multifile checking etc is useless, simply congeal with nothing during
         * the congeal process
         */
        {
            int numberOfFilesProcessed = 0;
            bool isSecondFileInMultiLine = true;
            bool isMultiLinePrt = true; //check if more than one prt file for batch
            bool onlyFile2Process = false; //if there's only one file, no point checking if its multifile
            PowerButton.Image = Properties.Resources.PowerActive;
            printToLog($"Writing {numberOfFiles2BeWrittenGlobal} .prt files to {currentSelectedUSBDrive}");
            for (int writerIndex = 0; writerIndex < numberOfFiles2BeWrittenGlobal; writerIndex++)
            //Loop that goes through each file to be printed and sends it to the write function
            {
                
                numberOfFilesProcessed++;
                string pathOfFile2BeWritten = (folderLocationBox.Text + "\\" + prtFilesToBeWritten[writerIndex]);
                string fileName2print = prtFilesToBeWritten[writerIndex];
                try
                {
                    string[] batchNumber1array = prtFilesToBeWritten[writerIndex].Split("_");
                    string batchNumber1 = batchNumber1array[batchNumber1array.Length - 2];
                    string[] batchNumber2array = prtFilesToBeWritten[writerIndex + 1].Split("_");
                    string batchNumber2 = batchNumber2array[batchNumber1array.Length - 2];
                    if (batchNumber1 != batchNumber2)
                    {
                        onlyFile2Process = false;
                    }
                }
                catch(Exception ex)
                {
                    printToLog($"{ex.Message}");
                    onlyFile2Process = false;
                }
                printToLog($"Writing file {fileName2print} to {currentSelectedUSBDrive}");
                
                string USBPath2CheckForMultiLine = copyOverPrtFile(pathOfFile2BeWritten, currentSelectedUSBDrive, fileName2print);
                if (!onlyFile2Process) //if more than 1 file
                {
                    isMultiLinePrt = moreThanOnePrtOfThisFileNameOnUsbOrNot(USBPath2CheckForMultiLine);
                    try
                    {
                        isSecondFileInMultiLine = secondFileMultiCheck(prtFilesToBeWritten[writerIndex], prtFilesToBeWritten[writerIndex + 1]);
                        printToLog($"File has identical file in next array space");
                    }
                    catch(Exception Ex)
                    {
                        printToLog($"No file in next line: {Ex.Message}");
                        isSecondFileInMultiLine = false;
                    }
                    //routine to check if this is multiple prt files in one batch
                }
                

                //copy over prts first to check in a moment
                prtTo425CSV(pathOfFile2BeWritten, currentSelectedUSBDrive, (writerIndex), isMultiLinePrt, isSecondFileInMultiLine, fileName2print);
                //Write files to temporary .csv files starting with "2congealB[batch number]_[prtPartNumber] if multiLine

                //Copy original .prt files for comparison with drive in future checks:
                //need a routine to try and check for index behind, and compare filenames, throw exception if not and print "no file underneath this one"
            }
           
            //Files are now written as temporary congeal files, so now check if anything contains that initial name, split at the end (there is always a _1 file), and
            //for each 2congealxxxx file, search for the whole string, if there's more than 1 result then add all of them to an array, cycle through the array starting at index 1
            //and continue appending to index 0
            //at the end of this, write to a new file with just the full list and delete the congeal files associated with original, then rerun check for more congeal files.

            CongealSameBatchFiles(currentSelectedUSBDrive);
            clearAllTempFiles(currentSelectedUSBDrive);
            //in routine, split between ; symbols, then write the cutting number to be the count of files congealed
            

            //End of Write
            PowerButton.Image = Properties.Resources.PowerIdle;
            CheckUSBProcess();
        }
        private bool secondFileMultiCheck(string currentFile, string nextFileInArray)
        /*This has ended up a little bit complicated, spaghetti and insane, but the idea here was to check if there were other files on the USB already
         * with the same PRT file associated. The only problem is, I've sorted everything into a loop that should stay as a loop to help me out and
         * function well, but only one prt is written to the usb per loop.
         * This means that on the first file write, the USB files cannot return that there are duplicates of a prt, as there will always be just
         * one file to check and no comparison to make.
         * This means I have to check that the current file and the next one to be written are the same instead, which works through throwing an
         * exception if its out of bounds and essentially ignoring it. If there is an exception (I.e. there's not a file after the first, which can only apply
         * to the first file), it will just not raise this flag. The flag for this or for original multiline checkings will allow the congeal files to 
         * start being written. It's extremely roundabout and complicated but this is the best way...
         * 
         */
        {
            string[] batch2Check = currentFile.Split("_");
            string[] nextBatch2Check = nextFileInArray.Split("_");
            if (batch2Check[0] == nextBatch2Check[0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool moreThanOnePrtOfThisFileNameOnUsbOrNot(string USBPATH)
        {
            string[] batchNumberArray = USBPATH.Split("_");
            string batchNumber = batchNumberArray[batchNumberArray.Length - 2];
            batchNumber = batchNumber.Replace($"{currentSelectedUSBDrive}", "");
            string[] fileNames = Directory.GetFiles(currentSelectedUSBDrive, $"*{batchNumber}*", SearchOption.TopDirectoryOnly);
            if (fileNames.Length > 1)
            {
                return true;
            }
            else
            {
                return false; //file is alone, and NOT multiPrt
            }
        }

        private void prtTo425CSV(string pathOfPCFile, string targetUSB, int fileNumber, bool isMultiLinePrt, bool isSecondFileMultiPart, string fileName)
        {
            //-------DEFAULTS AND INITIALISATION--------//
            int cuttingNo = 1;
            string cuttingNoAsString = $"{cuttingNo}";
            string cuttingLength = "000.0"; //Size of cut to make
            string profileCode = "LANXXX"; //E.G. LAN106W, found on line 7 after AB^FD
            string leftHead = "45 | 90"; //based on |-| style drawing, run method to return number based on character, found just before "Right"
            string rightHead = leftHead; //These are copies of each other
            const string amount = "1"; //always 1
            string height = "60"; //Can query db to find out, but just leave at 70 for most part
            string orderNumber = "J0024569"; //Taken from Job No, after No: and before ^FS (do a double split)
            string poz = "1"; //Not sure if matters, leave at 1.
            string assembly = ($"{fileNumber + 1}"); //same as cuttingNo, changes with file, but shouldnt matter too much.
            //Ignore Trolley, Box axis, Gasket, Operation//
            string dealer = "1234567"; //Barcode

            //Array for compress into final .csv
            string detailsAsOneLine = "";


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
                printToLog($"Cutting Number: {cuttingNoAsString}");
                //------------------------------//

                //------------PART 2------------//
                //------Get cutting length------//
                var prtLine8Array = contentsOfPrt[7].Split(' ');
                cuttingLength = prtLine8Array[1];
                if((cuttingLength.Substring(0, 1) == @"|") || (cuttingLength.Substring(0, 1) == @"-") || 
                    (cuttingLength.Substring(0, 1) == @"\") || (cuttingLength.Substring(0, 1) == @"/"))
                {
                    cuttingLength = "0"; //If cutting length is missing, hence the angle would be loaded instead
                }
                cuttingLength = cuttingLength.Substring(0, cuttingLength.Length - 1); //remove letter at the end
                //Add .0 decimal if no decimal found
                try
                {
                    //Tries to split the number at the decimal point and assign the post decimal data to a variable
                    //If no decimal is in string, exception is thrown, which we catch and then assign a decimal of .0 in the catch statement
                    var testForDecimal = cuttingLength.Split('.');
                    string throwExceptionIfNoDecimal = testForDecimal[1];
                }
                catch (Exception ex)
                {
                    printToLog($"{ex.Message}");
                    printToLog($"Length has no decimal, appending .0");
                    cuttingLength = cuttingLength + ".0";

                }
          
                printToLog($"Cutting Length: {cuttingLength}");
                //------------------------------//

                //------------PART 3------------//
                //--------Get Profile Code------//
                var prtLine7Array = contentsOfPrt[6].Split(' '); //Split line 7 by spaces
                var line7part1FurtherSplit = prtLine7Array[0].Split("FD"); //Split the first section between before FD and after FD
                profileCode = line7part1FurtherSplit[1]; //profile code is the piece after FD but before the space
                printToLog($"Profile Code: {profileCode}");
                //------------------------------//

                //------------PART 4------------//
                //--------Get Head Angles-------// 
                var line8SplitAtAngleHyphen = contentsOfPrt[7].Split('-'); //split by hyphen
                string leftCharacterIcon = 
                line8SplitAtAngleHyphen[0].Substring(line8SplitAtAngleHyphen[0].Length - 2); //Grab two characters before hyphen (angle and space)
                string rightCharacterIcon = 
                line8SplitAtAngleHyphen[1].Substring(0, 2); //Grab two characters after hypen (space and right angle)
                leftCharacterIcon = leftCharacterIcon.Trim(); //Remove space
                rightCharacterIcon = rightCharacterIcon.Trim(); //Remove Space
                //Find angle from symbol//
                string leftSawAngle = findSawAngle(leftCharacterIcon);
                string rightSawAngle = findSawAngle(rightCharacterIcon);
                leftHead = $"{leftSawAngle}|{rightSawAngle}";
                rightHead = leftHead;
                printToLog($"Left Head: {leftCharacterIcon}-{rightCharacterIcon} -> {leftHead}");
                printToLog($"Right Head: {leftCharacterIcon}-{rightCharacterIcon} -> {rightHead}");
                //------------------------------//

                //Print Constants:
                printToLog($"Amount: {amount}");
                printToLog($"Height: {height}");
               

                //------------PART 5------------//
                //--------Get Job Number--------// 
                var prtLine5SplitAtNo = contentsOfPrt[4].Split("No:"); //split between 'no:'
                var prtLine5SplitMoreAtFs = prtLine5SplitAtNo[1].Split("^FS"); //further split between ^FS at end
                orderNumber = prtLine5SplitMoreAtFs[0]; //Just before ^FS is job number
                if(orderNumber == "") //If this is blank, print that there is none
                {
                    printToLog($"Job Number: None");
                }
                else
                {
                    printToLog($"Job Number: {orderNumber}");
                }
                //------------------------------//

                //Print Constants:
                printToLog($"Position: {poz}");
                printToLog($"Assembly: {assembly}");

                //------------PART 6----------------//
                //--------Get Dealer/Barcode--------//
                var prtLine4SplitAtName = contentsOfPrt[3].Split("Name: ");
                var prtLine4SplitMoreAtFs = prtLine4SplitAtName[1].Split("^FS"); //same process as job number but for line 4
                dealer = prtLine4SplitMoreAtFs[0];
                printToLog($"Dealer: {dealer}");
                //----------------------------------//

                //SIZE CHECKS/COMPRESSIONS//
                printToLog($"Checking sizes...");
                cuttingNoAsString = isThisTooBig(cuttingNoAsString, 4, "Cutting Number");
                cuttingLength = isThisTooBig(cuttingLength, 6, "Cutting Length");
                profileCode = isThisTooBig(profileCode, 12, "Profile Code");
                leftHead = isThisTooBig(leftHead, 5, "Left Head");
                rightHead = isThisTooBig(rightHead, 5, "Right Head");
                height = isThisTooBig(height, 3, "Height");
                orderNumber = isThisTooBig(orderNumber, 10, "Order Number");
                poz = isThisTooBig(poz, 3, "Position");
                assembly = isThisTooBig(assembly, 1, "Assembly");
                dealer = isThisTooBig(dealer, 21, "Dealer/Barcode");
                printToLog($"Finished Checking");
                //------------------------//


                //-----------PART 7----------------//
                //--------Writing to csv-----------//
                //Compile all information into one string line with ; seperating the values
                detailsAsOneLine = ($"{cuttingNoAsString};{cuttingLength};{profileCode};{leftHead};{rightHead};{height};{orderNumber};{poz};{assembly}" +
                                        $";;;;;;{dealer}");
                string nameOfFile2Write = fileName;
                nameOfFile2Write = nameOfFile2Write.Substring(0, nameOfFile2Write.Length - 4);
                
             
                //---------------------------------//  
                printToLog($"File is one part of a multi .prt batch");
                //append to start 2congealB
                nameOfFile2Write = ($"2congealB{nameOfFile2Write}.csv");
                printToLog($"Generated temporary file {nameOfFile2Write}");
              
                string fullWritePath = $"{currentSelectedUSBDrive}{nameOfFile2Write}";
                File.WriteAllText(fullWritePath, detailsAsOneLine);
               


            } //End of loop that checks if file exists (safety loop)
            else
            {
                printToLog($"File error? Can't find {pathOfPCFile}");
                printToLog($"Please refresh USB drives!");
            }
        }

        private string copyOverPrtFile(string originalPath, string USBDRIVE, string fileName)
        {

            string usbFilePath = ($"{USBDRIVE}{fileName}");
            printToLog($"Copying {fileName} to {USBDRIVE}");
            try
            {
                File.Copy(originalPath, usbFilePath);
            }
            catch(Exception ex)
            {
                printToLog($"Error copying {fileName}: {ex.Message}");
            }
            return usbFilePath;
        }
        private string isThisTooBig(string string2Check, int maximumLength, string variableType)
        {
            string shortenedString = string2Check; //default
            if (string2Check.Length > maximumLength)
            {
                printToLog($"{variableType} is too long! Shortening to first {maximumLength} characters");
                shortenedString = string2Check.Substring(0, (maximumLength - 1));
            }
            return shortenedString;
        }

        private string findSawAngle(string prtAngle)
        //function to convert saw icon to actual angle, returns cutting angle
        {
            string angle2return = "90"; //default
            if(prtAngle == @"\")
            {
                angle2return = "45";
            }
            else if(prtAngle == @"|")
            {
                angle2return = "90";
            }
            else if (prtAngle == @"/")
            {
                angle2return = "45";
            }
            else { angle2return = "90"; }

            return angle2return;
        }

        private void CongealSameBatchFiles(string USBPATH)
        //searches for 2congeal files and appends them to a root index
        {
            //---1, Generate Dynamic List of files that end in _1.csv---//
            string[] CongealFiles = Directory.GetFiles(USBPATH, "2congealB*", SearchOption.TopDirectoryOnly); //create array of all 2congeal Files
            var rootCongealFiles = new List<string> { };
            foreach (string originalFileName in CongealFiles)
            {
                string[] fileNameSplitUp = originalFileName.Split("_");
                if (fileNameSplitUp[fileNameSplitUp.Length - 1] == "1.csv")
                {

                    //Adds _1.csv file to a list of "root files"
                    rootCongealFiles.Add($"{originalFileName}");
                    printToLog($"Root File Found: {originalFileName}");
                }
                
            }

            //--2, for each root file - check if _2 exists, _3 (with counter) etc until end--//
            //If they do exist, append into giant string//
            

            foreach (string rootFile in rootCongealFiles) { //Cycle each root file
                //printToLog($"Reading contents of {rootFile}");
                //Generate filename without the _1 at the end
                string[] splitUpFileName = rootFile.Split("_1");
                string fileNameWithoutEnding = splitUpFileName[0];

                string fullCsvOutput = File.ReadAllText(rootFile); //append first line
                int counterDefault = 2; //default search counter

                while (File.Exists($"{fileNameWithoutEnding}_{counterDefault}.csv"))
                {
                    
                    printToLog($"Congealing contents of {fileNameWithoutEnding}_{counterDefault}.csv");
                    string contentsOfCongealSplit = File.ReadAllText($"{fileNameWithoutEnding}_{counterDefault}.csv");
                    fullCsvOutput = ($"{fullCsvOutput}{Environment.NewLine}{contentsOfCongealSplit}");
                    counterDefault++;
                }

                //Write Appended string to .csv
                string[] fileNameWithCongealedRemovedSplit = fileNameWithoutEnding.Split("congealB");
                string justRootNumber = fileNameWithCongealedRemovedSplit[1];

                //Write to final file
                printToLog($"Generating {justRootNumber}.csv");
                string fullWritePath = ($"{USBPATH}{justRootNumber}.csv");
                //Check if file already exists, and delete it if so in order to rebuild file from scratch
                if (File.Exists(fullWritePath))
                {
                    printToLog($"{justRootNumber}.csv already exists - rewriting...");
                    File.Delete(fullWritePath);
                }
                File.WriteAllText(fullWritePath, fullCsvOutput);
                


            }


        }

        private void clearAllTempFiles(string USBPATH)
        //Find and delete and temporary congeal files
        {
            printToLog($"Clearing up congeal files on {USBPATH}");
            string[] CongealFiles = Directory.GetFiles(USBPATH, "2congealB*", SearchOption.TopDirectoryOnly); //create array of all 2congeal Files
            foreach(string file2Delete in CongealFiles)
            {
                if (File.Exists(file2Delete))
                {
                    File.Delete(file2Delete);
                }
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

        /*
         * Why use congeal/temporary files?
         * While the process of invoking temporary files is overall slower and more CPU intensive than just appending everything to one variable
         * then writing the final output to one file, but it makes it easier for me to segment and parse each section of code.
         * The conversion to .csv takes place in a loop that could be amended to combine into one giant string, but there's also the issue of checking
         * which files contain extra parts or are standalone. Again, this could also be done in one loop, but to keep everything clean and
         * easy to work with from my perspective, I'd much prefer to have everything converted, THEN begin the congeal process in a seperate
         * function.
         * This isn't optimized, but it does make it easier for me to mentally grasp. The amount of boolean flags/checks and loops I've included up to
         * the point of conversion is a lot to keep track of in my head, and I'd rather sacrifice a little bit of speed to help me actually
         * finish the program and keep track of it. The files are all relatively small so the IO speed suffering shouldn't be too great.
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
