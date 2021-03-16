using System;
using System.IO;
using System.Text;
using System.Diagnostics;



namespace Phonebook
{
    public class FileIO
    {
        public static PersonAccaunt p = new PersonAccaunt();
        #region ShowMainMenu
        /// <summary>
        /// The method displays the main menu of the program.
        /// Depending on the user's choice, the method of the corresponding menu item is called. 
        /// To exit the program, you need to type "enter". 
        /// The method works by checking the correctness of the entered menu item.
        /// </summary>
        public static void ShowMainMenu()
        {
            string ch;
            bool exitOk = false;
            while (!exitOk)
            {
                Console.Clear();
                Console.Write("**********************The program PhonBook************************\n");
                Console.Write("==================================================================\n\n");
                Console.Write("             For end of work, type: \"exit\".\n\n");
                Console.Write("|===================================|============================|\n");
                Console.Write("|  1)   Create New PhoneBook        |    2)  Delete PhoneBook    |\n");
                Console.Write("|===================================|============================|\n");
                Console.Write("|  3)   Edit PhoneBook              |    4)  Copy PhoneBook      |\n");
                Console.Write("|===================================|============================|\n");
                Console.Write("|  5)   Print PhoneBook             |    6)  Rename PhoneBook    |\n");
                Console.Write("|================================================================|\n");
                Console.Write("|                     7) Find some thing in PhoneBook            |\n");
                Console.Write("|================================================================|\n");

                Console.Write("Make your choice :   "); ch = Console.ReadLine();
                if (ch == "exit") { exitOk = true; break; }
                else
                {
                    switch (ch)
                    {
                        case "1":
                            {
                                CreateNewPhoneBook();
                                break;
                            }
                        case "2":
                            {
                                DeletePhoneBook();
                                break;
                            }
                        case "3":
                            {
                                EditPhoneBook();
                                break;
                            }
                        case "4":
                            {
                                CopyPhoneBook();
                                break;
                            }
                        case "5":
                            {
                                PrintConsolePhoneBook();
                                break;
                            }
                        case "6":
                            {
                                RenamePhoneBook();
                                break;
                            }
                        case "7":
                            {
                                FindSomeThingInPhoneBook();
                                break;
                            }
                        case "exit":
                            {
                                exitOk = true; break;
                            }
                        default:
                            {
                                Console.WriteLine("Error menue case. Try again, or type \" exit \" to stop program.");
                                break;
                            }
                    }
                    exitOk = false;
                }
            }
        }
        #endregion
        #region GetPathForFile
        /// <summary>
        /// The method asks for the path to an existing or future phone book file phonebook.txt
        /// </summary>
        /// <returns></returns>
        public static string GetPathForFile()
        {
            Console.Write("Input path for file phonebook.txt:   ");
            return Console.ReadLine();
        }
        #endregion

        #region 1_CreateNewPhoneBook
        /// <summary>
        /// This method, before starting its work, asks the user type absolute path for file of the future phonebook by wich it will be created. 
        /// If this path exists, the method displays message about this, otherwise it offers to create a folder with a file in the current directory.
        /// After that, it requests with verification from the user abonent's data and displays their final set on the screen.
        /// If everything is correct, data is added to the phonebook, otherwise user can enter the data again.
        /// </summary>
        public static void CreateNewPhoneBook()
        {
            StringBuilder sb = new StringBuilder();
            var currentDirectory = Directory.GetCurrentDirectory();
            var phoneBookDirectory = Directory.CreateDirectory(Path.Combine(currentDirectory, "PhoneBookDir"));
            var continueInputOk = true;
            string abonentName, abonentLastName, abonentCity, abonentStreet;
            uint abonentIndex, abonentHous, abonentApartment, abonentPhoneNamber;
            while (continueInputOk)
            {
                var enteredPath = GetPathForFile();
                try
                {
                    if (Directory.Exists(enteredPath))
                    {
                        Console.WriteLine("Directory exist.");
                        try
                        {
                            var phoneBookDirectory1 = Directory.CreateDirectory(Path.Combine(enteredPath, "PhoneBookDir"));
                            phoneBookDirectory = phoneBookDirectory1;
                            continueInputOk = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Imposible to create file by entered path.\n");
                            Console.WriteLine(ex.Message + "\n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Imposible to create file by entered path.\n");
                        Console.WriteLine("Create new Phone book in this directory: \n" + phoneBookDirectory + "   ?   y/n     ");
                        if (Console.ReadLine() == "y")
                        {
                            Console.WriteLine("File whill be created by this path:    \n" + phoneBookDirectory);
                            continueInputOk = false;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Imposible to create file by this path.\n");
                    Console.WriteLine(ex.ToString() + "\n\n");
                }

            }

            continueInputOk = true;
            while (continueInputOk)
            {
                CheckInputDataPerson(out abonentName,
                                     out abonentLastName, 
                                     out abonentCity, 
                                     out abonentIndex, 
                                     out abonentStreet, 
                                     out abonentHous, 
                                     out abonentApartment, 
                                     out abonentPhoneNamber);

                var person = new PersonAccaunt(abonentName,
                                               abonentLastName,
                                               abonentCity, 
                                               abonentIndex,
                                               abonentStreet, 
                                               abonentHous, 
                                               abonentApartment,
                                               abonentPhoneNamber);
                DisplayPersonAccaunt(person);
                Console.WriteLine("===============================================\n");
                Console.Write("Is all right? Add this person in Phone Book? y/n     ");
                if (Console.ReadLine() == "y")
                {
                    p = person;
                    //person = null;
                    WritePersonInFile(sb, phoneBookDirectory);
                    Console.WriteLine("Abonent added...");
                    Console.Write("Add new abonent? y/n ");
                    if (Console.ReadLine() == "y") Console.Clear();
                    else continueInputOk = false;
                }

            }
            ShowMainMenu();
            
            #region CheckInputDataPerson
            /// <summary>
            /// Block for checking entered numeric and integer data
            /// </summary>

            static void CheckInputDataPerson(out string abonentName,
                                             out string abonentLastName, 
                                             out string abonentCity,
                                             out uint abonentIndexi, 
                                             out string abonentStreet, 
                                             out uint abonentHous, 
                                             out uint abonentApartment, 
                                             out uint abonentPhoneNamber)
            {
                CheckOnCorrectStringData("First Name", out abonentName);
                CheckOnCorrectStringData("Last Name", out abonentLastName);
                CheckOnCorrectStringData("City", out abonentCity);
                CheckOnCorrectIntData("Post index", out abonentIndexi);

                CheckOnCorrectStringData("Street", out abonentStreet);
                CheckOnCorrectIntData("Number of hous", out abonentHous);
                CheckOnCorrectIntData("Number of apartment", out abonentApartment);
                Console.WriteLine("Enter mobil phone number after zero : +380");
                CheckOnCorrectIntData("Phone number", out abonentPhoneNamber);
            }
            #endregion
        }
        #region WritePersonInFile
        /// <summary>
        /// Block of writing data to a file
        /// </summary>
        /// <param name="sb">temporary data storage</param>
        /// <param name="phoneBookDirectory">The path to the file</param>
        private static void WritePersonInFile(StringBuilder sb, DirectoryInfo phoneBookDirectory)
        {
            sb.AppendLine("========================================");
            sb.AppendLine("First name            :  " + p.FirstName);
            sb.AppendLine("Last name             :  " + p.LastName);
            sb.AppendLine("City                  :  " + p.CityOfResidence);
            if (p.PostIndex == 0)
                sb.AppendLine("Post index            :  unknown");
            else
                sb.AppendLine("Post index            :  " + p.PostIndex);
            sb.AppendLine("Street                :  " + p.CityStreet);
            if (p.NumberOfHous == 0)
                sb.AppendLine("Hous                  :  unknown");
            else
                sb.AppendLine("Hous                  :  " + p.NumberOfHous);
            if (p.ApartmentNumber == 0)
                sb.AppendLine("Apartment             :  unknown");
            else
                sb.AppendLine("Apartment             :  " + p.ApartmentNumber);

            if (p.MobilPhone == 0)
                sb.AppendLine("Phone                 :  unknown");
            else
                sb.AppendLine("Phone                 :  +380" + p.MobilPhone);

            sb.AppendLine("Time of registration  :  " + p.DateOfRegistration);
            sb.AppendLine("Date of registration  :  " + p.TimeOfRegistration);

            foreach (var s in sb.ToString())
            {
                File.AppendAllText(Path.Combine(Convert.ToString(phoneBookDirectory), "phonebook.txt"), $"{s}");
            }
            sb.Clear();
        }
        #endregion
        #endregion
        #region 2_DeletePhoneBook
        /// <summary>
        /// Delete the phonebook at the entered path
        /// </summary>
        public static void DeletePhoneBook()
        
        {
            var enteredPath = GetPathForFile();
            enteredPath = enteredPath + "\\phonebook.txt";
            if (File.Exists(enteredPath))
            {
                Console.WriteLine("File exist.");
                try
                {
                    Console.Write("Delete this file: " + enteredPath + " ?   y/n    ");
                    if (Console.ReadLine() == "y")
                    {
                        File.Delete(enteredPath);
                        Console.WriteLine("File deleted...");
                        System.Threading.Thread.Sleep(3000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Imposible to delate file by entered path.\n");
                    Console.WriteLine(ex.Message + "\n\n");
                    System.Threading.Thread.Sleep(8000);
                }
            }
            else if (!File.Exists(enteredPath))
            {
                Console.WriteLine("File not exist.");
                System.Threading.Thread.Sleep(3000);
            }
            Console.Clear();
            ShowMainMenu();
        }
        #endregion
        #region 3_EditPhoneBook
        /// <summary>
        /// Opens a phonebook file located at entered path using Notepad for editing, if file exist
        /// Notepad is assumed to be in the Windows folder c:\Windows\
        /// </summary>
        public static void EditPhoneBook()
        {
            string fullSourseFilePath;
            var soursFilePath = GetPathForFile();
            fullSourseFilePath = soursFilePath + "\\phonebook.txt";
            if (File.Exists(fullSourseFilePath))
            {
                try
                {
                    Process proc = new Process();
                    string notepadExe = "";
                    string[] dirs = Directory.GetFiles(@"c:\Windows\", "notepad1.exe");
                    if ((dirs[0] != null) && (dirs[0] != ""))
                    {
                        notepadExe = dirs[0];
                        Console.WriteLine("Open file {0} ...", fullSourseFilePath);
                        Console.WriteLine("Edit and save file.");
                        System.Threading.Thread.Sleep(3000);
                        Process.Start(Convert.ToString(notepadExe), Convert.ToString(fullSourseFilePath));
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Path for notepad.exe not found...");
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }


            }
            else
            {
                Console.WriteLine("File not exist.");
                System.Threading.Thread.Sleep(3000);
            }
            Console.Clear();
            ShowMainMenu();
        }

        #endregion
        #region 4_CopyPhoneBook
        /// <summary>
        /// Copies a file, if file exists, from one specified location to another specified location. 
        /// If the destination path does not exist it can be created. 
        /// If the file at the specified path already exists, it can be overwritten.
        /// </summary>
        public static void CopyPhoneBook()
        {
            string fullSourseFilePath, fullTargetFilePath, targetFilePath;
            var soursFilePath = GetPathForFile();
            fullSourseFilePath = soursFilePath + "\\phonebook.txt";
            if (File.Exists(fullSourseFilePath))
            {

                Console.WriteLine("File exist.");
                Console.Write("Input path for copy file:    ");
                targetFilePath = Console.ReadLine();
                var targetPathExist = false;
                if (Directory.Exists(targetFilePath))
                {
                    Console.WriteLine("Path exist.");
                    targetPathExist = true;
                }
                else
                {
                    Console.Write("Path not exist. Create this path?    y/n     ");
                    if (Console.ReadLine() == "n")
                    {
                        Console.WriteLine("Ok. Make another chois.");
                        System.Threading.Thread.Sleep(3000);
                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(targetFilePath);
                            Console.WriteLine("Path {0} created...", targetFilePath);
                            targetPathExist = true;
                            System.Threading.Thread.Sleep(3000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Imposible to create this path.");
                            Console.WriteLine(ex.Message);
                            System.Threading.Thread.Sleep(8000);
                        }
                    }
                }
                if (targetPathExist)
                {
                    if (soursFilePath != targetFilePath)
                        try
                        {
                            fullTargetFilePath = targetFilePath + "\\phonebook.txt";
                            if (File.Exists(fullTargetFilePath))
                            {
                                Console.Write("File {0} exist. Overwrite?   y/n ", fullTargetFilePath);
                                if (Console.ReadLine() == "n")
                                {
                                    Console.WriteLine("Copy file dinied...");
                                    System.Threading.Thread.Sleep(3000);
                                }
                                else
                                {
                                    File.Copy(fullSourseFilePath, fullTargetFilePath, true);
                                    Console.WriteLine("File copied...");
                                    System.Threading.Thread.Sleep(3000);

                                }
                            }
                            else
                            {
                                File.Copy(fullSourseFilePath, fullTargetFilePath);
                                Console.WriteLine("File copied...");
                                System.Threading.Thread.Sleep(3000);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Imposible to copy file by entered path.\n");
                            Console.WriteLine(ex.Message + "\n\n");
                            System.Threading.Thread.Sleep(20000);
                        }
                    else
                    {
                        bool f;
                        Console.Write("Make a copy file phonebook1.txt?  y/n     ");
                        if (Console.ReadLine() == "y")
                        {
                            f = true;
                            fullTargetFilePath = targetFilePath + "\\phonebook1.txt";
                        }
                        else
                        {
                            f = false;
                            Console.Write("Input new name fo file copy: ");
                            var newFileName = Console.ReadLine();
                            fullTargetFilePath = targetFilePath + "\\" + newFileName;
                        }
                        try
                        {

                            File.Copy(fullSourseFilePath, fullTargetFilePath, overwrite: f);
                            Console.WriteLine("File copied...");
                            System.Threading.Thread.Sleep(3000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\nImposible to copy file by entered path.");
                            Console.WriteLine(ex.Message + "\n\n");
                            System.Threading.Thread.Sleep(8000);
                            Console.ReadKey();
                        }

                    }
                }

            }
            else if (!File.Exists(fullSourseFilePath))
            {
                Console.WriteLine("File not exist.");
                System.Threading.Thread.Sleep(3000);
            }
            Console.Clear();
            ShowMainMenu();
        }

        #endregion
        #region 5_PrintConsolePhoneBook
        /// <summary>
        /// Displays contents of the phonebook the specified path on the screen
        /// </summary>
        public static void PrintConsolePhoneBook()
        {
            var enteredPath = GetPathForFile();
            enteredPath = enteredPath + "\\phonebook.txt";
            if (!File.Exists(Convert.ToString(enteredPath)))
            {
                Console.WriteLine("Error 404. File not found.   ");
                System.Threading.Thread.Sleep(8000);
            }
            else
            {
                Console.WriteLine("File exist.");
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
                AllFromFileToScreen(enteredPath);
                Console.ReadLine();
            }
            Console.Clear();
            ShowMainMenu();
        }
        #endregion
        #region 6_RenamePhoneBook
        /// <summary>
        /// Renames the phone book file at the specified path
        /// </summary>
        public static void RenamePhoneBook()
        {
            var targetPath = GetPathForFile();
            var fullSourseFilePath = targetPath + "\\phonebook.txt";
            if (!File.Exists(Convert.ToString(fullSourseFilePath)))
            {
                Console.WriteLine("Error 404. File not found.   ");
                System.Threading.Thread.Sleep(8000);
            }
            else
            {
                Console.Write("Input new name for file  {0}    ", fullSourseFilePath);
                var newFileName = Console.ReadLine();
                targetPath = targetPath + "\\" + newFileName;
                try
                {
                    File.Copy(fullSourseFilePath, targetPath, overwrite: true);
                    Console.WriteLine("File renamed...");
                    File.Delete(fullSourseFilePath);
                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Imposible to rename file.");
                    Console.WriteLine(ex.Message);
                    System.Threading.Thread.Sleep(8000);
                }
            }
            Console.Clear();
            ShowMainMenu();
        }
        #endregion
        #region 7_FindSomeThingInPhoneBook
        /// <summary>
        /// Search for a abonent by a specified parameter
        /// </summary>
        public static void FindSomeThingInPhoneBook()
        {
                var enteredPath = GetPathForFile();
                enteredPath += "\\phonebook.txt";
                if (!File.Exists(Convert.ToString(enteredPath)))
                {
                    Console.WriteLine("Error 404. File not found.   ");
                    System.Threading.Thread.Sleep(8000);
                }
                else
            {
                ShowMenueForFindSomeThing(enteredPath);
            }
            Console.Clear();
            ShowMainMenu();
            #region FindAndShowSomeThing
            /// <summary>
            /// The method sequentially reads from the specified file a block of 10 lines related to a abonent
            /// and checks for a match with the content. If a match is found, the subscriber data is displayed
            /// </summary>
            static void FindAndShowSomeThing(string pathToFile, string choisCase)
                {
                    var personDivider = "\n===============================================";
                    StringBuilder sb = new StringBuilder();
                    bool continueOk;
                    FileStream fin;
                    string s;
                    Console.Write("Input {0}:   ", choisCase);
                    var fildStr = Console.ReadLine();
                    
                        try
                        {
                            fin = new FileStream(pathToFile, FileMode.Open);
                            Console.WriteLine("File opening...");
                            System.Threading.Thread.Sleep(3000);
                        }
                        catch (IOException exc)
                        {
                            Console.WriteLine("Error open file:" + exc.Message);
                            return;
                        }
                        StreamReader fstr_in = new StreamReader(fin);
                        try
                        {
                            do
                            {
                                continueOk = true;
                                s = fstr_in.ReadLine();
                                if ((s != personDivider) & (s != null))
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        s = fstr_in.ReadLine();
                                        sb.AppendLine(s);
                                    }
                                }
                                else if (s == null) continueOk = false;
                                
                                s = sb.ToString();
                                sb.Clear();
                                if (s.Contains(fildStr))
                                {
                                     Console.WriteLine(personDivider);
                                     Console.WriteLine(s);
                                }
                                s = "";
                            }
                            while (continueOk);
                        }
                        catch (IOException exc)
                        {
                            Console.WriteLine("Error input/output:\n" + exc.Message);
                        }
                        finally
                        {
                            fstr_in.Close();
                        }
            }
            #region ShowMenueForFindSomeThing
            /// <summary>
            /// Displays a menu for searching for something in the phonebook
            /// </summary>
            static void ShowMenueForFindSomeThing(string toFind)
            {
                string chStr;
                bool exitOk = false;
                Console.WriteLine("File exist.");
                while (!exitOk)
                {
                    Console.Clear();
                    Console.Write("*****************************Find person by*****************************\n");
                    Console.Write("========================================================================\n\n");
                    Console.Write("                   For end of work, type: \"exit\".\n\n");
                    Console.Write("|===================================|===================================|\n");
                    Console.Write("|  1)   Name                        |    2)  Last name                  |\n");
                    Console.Write("|===================================|===================================|\n");
                    Console.Write("|  3)   City                        |    4)  Street                     |\n");
                    Console.Write("|===================================|===================================|\n");
                    Console.Write("|  5)   Hous                        |    6)  Apartment                  |\n");
                    Console.Write("|=======================================================================|\n");
                    Console.Write("|                            7) Phone number                            |\n");
                    Console.Write("|=======================================================================|\n");
                    Console.Write("Make your choice :   "); chStr = Console.ReadLine();
                    string choisStr;
                    Console.WriteLine(chStr);
                        switch (chStr)
                        {
                            case "1":
                                {
                                    choisStr = "First name";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "2":
                                {
                                    choisStr = "Last name";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "3":
                                {
                                    choisStr = "City";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "4":
                                {
                                    choisStr = "Street";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "5":
                                {
                                    choisStr = "Hous";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "6":
                                {
                                    choisStr = "Apartment";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "7":
                                {
                                    choisStr = "Phone";
                                    FindAndShowSomeThing(toFind, choisStr);
                                    break;
                                }
                            case "exit":
                                {
                                    Console.WriteLine(exitOk);
                                    exitOk = true;
                                    return;
                                }
                            default:
                                {
                                    Console.WriteLine("Error menue case. Try again, or type \" exit \" to stop procedure.");
                                    break;
                                }

                        }
                        Console.ReadKey();
                }
            }
            #endregion
            #endregion
        }
        #endregion

        #region AddNewAccauntPersonInPhoneBook
        /// <summary>
        /// Writing data to a file using streams
        /// </summary>
        /// <param name="p">class instance</param>
        /// <param name="pathToFile">the path to the file</param>
        public static void AddNewAccauntPersonInPhoneBook(PersonAccaunt p, string pathToFile)
        {
            FileStream fout;    // Open file I / O stream first
            try
            {
                fout = new FileStream(pathToFile, FileMode.Append);
            }
            catch (IOException exc)
            {
                Console.WriteLine("Error open file: " + exc.Message);
                return;
            }
            //Enclose file's I/O stream in the StreamWriter class.
            StreamWriter fstr_out = new StreamWriter(fout);
            try
            {
                fstr_out.Write("---------------------------------------\r\n");
                fstr_out.Write("First name            :  {0}\r\n", p.FirstName);
                fstr_out.Write("Last name             :  {0}\r\n", p.LastName);
                fstr_out.Write("City                  :  {0}\r\n", p.CityOfResidence);
                fstr_out.Write("Post index            :  {0}\r\n", p.PostIndex);
                fstr_out.Write("Street                :  {0}\r\n", p.CityStreet);
                fstr_out.Write("Hous                  :  {0}\r\n", p.NumberOfHous);
                fstr_out.Write("Apartment             :  {0}\r\n", p.ApartmentNumber);
                fstr_out.Write("Phone                 :  +380{0}\r\n", p.MobilPhone);
                fstr_out.Write("Time of registration  :  {0}\r\n", p.DateOfRegistration);
                fstr_out.Write("Date of registration  :  {0}\r\n", p.TimeOfRegistration);
                fstr_out.Write("---------------------------------------\r\n");
            }
            catch (IOException exc)
            {
                Console.WriteLine("Ошибка ввода-вывода:\n" + exc.Message);
            }
            finally
            {
                fstr_out.Close();
            }
        }
        #endregion
        #region AllFromFileToScreen
        /// <summary>
        /// Output from file to screen via FileStream
        /// </summary>
        /// <param name="pathToFile"></param>
        public static void AllFromFileToScreen(string pathToFile)
        {
            FileStream fin;
            string s;
            try
            {
                fin = new FileStream(pathToFile, FileMode.Open);
            }
            catch (IOException exc)
            {
                Console.WriteLine("Error open file:" + exc.Message);
                return;
            }
            StreamReader fstr_in = new StreamReader(fin);
            try
            {
                while ((s = fstr_in.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            catch (IOException exc)
            {
                Console.WriteLine("Error input/output:\n" + exc.Message);
            }
            finally
            {
                fstr_in.Close();
            }
        }
        #endregion

        //===========================
        //       InputAndCheck
        //===========================

        #region Check String Data
        /// <summary>
        /// checking string data for length
        /// </summary>
        /// <param name="nameP">name of parametr</param>
        /// <param name="param">value of parametr</param>
        static void CheckOnCorrectStringData(string nameP, out string param)
        {
            string p = "";
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write("Enter abonent {0}    : ", nameP); p = Console.ReadLine();
                if (p.Length > 15)
                {
                    Console.Write("Error! {0} length exceeds 15 characters! Do you wish try again? y/n  ", nameP);
                    if (Console.ReadLine() == "n")
                    {
                        Console.WriteLine("{0} whill be assined like unknown.", nameP);
                        p = "unknown";
                        break;
                    }
                    else Console.WriteLine("Input, please, correct {0}.", nameP);
                }
                else isCorrect = true;
            }
            param = p;
            Console.WriteLine();
        }
        #endregion
        #region Check Int Data
        /// <summary>
        /// checking the correctness of numerical data
        /// </summary>
        /// <param name="nameP">name of parametr</param>
        /// <param name="param">value of parametr</param>
        static void CheckOnCorrectIntData(string nameP, out uint param)
        {
            uint p = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write("Enter abonent {0} : ", nameP);
                try
                {
                    p = uint.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch
                {
                    Console.Write("Uncorrect {0}. Do you wish try again? y/n  ", nameP);
                    if (Console.ReadLine() == "n")
                    {
                        Console.WriteLine("{0} whill be assined like unknown.", nameP);
                        p = 0;
                        break;
                    }
                    else Console.WriteLine("Input, please, correct {0}.", nameP);
                }

            }
            param = p;
            Console.WriteLine();
        }
        #endregion
        #region Method that Display Person Accaunt data on Console
        /// <summary>
        /// Method for displaying the current values ​​of class fields
        /// </summary>
        /// <param name="p"></param>
        public static void DisplayPersonAccaunt(PersonAccaunt p)
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("First name            :  {0}", p.FirstName);
            Console.WriteLine("Last name             :  {0}", p.LastName);
            Console.WriteLine("City                  :  {0}", p.CityOfResidence);

            if (p.PostIndex == 0)
                Console.WriteLine("Post index            :  unknown");
            else
                Console.WriteLine("Post index            :  {0}", p.PostIndex);

            Console.WriteLine("Street                :  {0}", p.CityStreet);

            if (p.NumberOfHous == 0)
                Console.WriteLine("Hous                  :  unknown");
            else
                Console.WriteLine("Hous                  :  {0}", p.NumberOfHous);

            if (p.ApartmentNumber == 0)
                Console.WriteLine("Apartment             :  unknown");
            else
                Console.WriteLine("Apartment             :  {0}", p.ApartmentNumber);

            if (p.MobilPhone == 0)
                Console.WriteLine("Phone                 :  unknown");
            else
                Console.WriteLine("Phone                 :  +380{0}", p.MobilPhone);

            Console.WriteLine("Time of registration  :  {0}", p.DateOfRegistration);
            Console.WriteLine("Date of registration  :  {0}", p.TimeOfRegistration);
        }
        #endregion
    }
}



