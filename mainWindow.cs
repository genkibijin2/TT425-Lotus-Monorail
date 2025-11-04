using System;
using System.IO;
using System.Windows.Forms;
namespace TT425_Lotus_Monorail
{
    public partial class mainWindow : Form
    {
        /*===GLOBAL VARIABLES===*/
        //####
        String defaultDirectory = @"C:\sawfiles_test"; //default directory for .prt files, change to C:\sawfiles_two when live
        string currentSelectedUSBDrive = ""; //global that holds the current USB drive letter/path, starts blank
        //####

        /*===BOOLEANS AND ERROR CHECKERS===*/
        //#
        Boolean removableDrivesAvailable = false; //Becomes true if USB/Removable Drives available
        Boolean sawFolderExists = false; //Becomes true if the default directory exists.
        Boolean sawFolderFilesArePRT = false;   //Becomes true if there are .prt files in the saw files folder
        Boolean USBComparisonSuccessful = false; //becomes true if USB has been checked and there are files ready to write
        Boolean readyToStartWriting = false; //Becomes true if it is okay to begin writing files
        Boolean sawFileNotYetWritten = false;   //Becomes true if the saw file does not also exist on target removable drive
        Boolean extremeErrorState = false; //Becomes true if an exception is thrown that should stop the program from continuing
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
            checkRemovableDrives();
        }

        private void removableDrivesSelection_SelectedIndexChanged(object sender, EventArgs e)
        //If dropdown changed, only perform a check if there's removable drives accessible
        {
            CheckUSBProcess();
        }

        private void changeFolderLocation()
        {
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
                compareFolderFilesToUSB(chosenStartFolder, USBDriveToChangeTo);

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

        private void compareFolderFilesToUSB(string locationToCheck, string USBToCheck)
        //This method is only reached when a USB is available and .prt files are found in the chosen folder
        //So, don't worry about not having access to drive names or folder paths - They are already checked
        {
            USBComparisonSuccessful = false; //RESET FLAG
            printToLog($"Comparing contents of {locationToCheck} to USB Drive {USBToCheck}");




            //Put this flag change wherever the comparison is successful...
            USBComparisonSuccessful = true;
            if (USBComparisonSuccessful)
            {
                //SUCCESS STATE - WRAP WITH SUCCESS FLAG AFTER FINISHING
                USBStatusText.Text = $"Ready To Write";
                USBCheckLight.Image = Properties.Resources.FileCheckOk;
                //-----------------------------------------------------------------------------------//
            }
        }








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
        //--------------------------------------------------------------------------------------------//
    }
}
