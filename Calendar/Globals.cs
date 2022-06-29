using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MySql.Data.MySqlClient;
using Calendar.ExtensionViews;
using Calendar.Views;
using Calendar.Elements;


namespace Calendar
{
    public class Globals
    {
        public static DateTime date;
        public static DateTime dateNow;
        public static System.Globalization.Calendar calendar;
        public static int numWeek;
        public static int numDayinWeek;
        public static bool przestepny;
        public static MySqlConnection conn;
        public static MySqlCommand command;
        public static MySqlDataReader reader;
        public static String sql, Output = "";
        public static int taskId = 0;
        public static bool isEnglishLanguage;
        public static string weekday;

        public static Dictionary<string, int> WeekDict = new Dictionary<string, int>()
        {
            { "Monday", 1},
            { "Tuesday", 2},
            { "Wednesday", 3},
            { "Thursday", 4},
            { "Friday", 5},
            { "Saturday", 6},
            { "Sunday", 7},
        };

        public static Dictionary<string, int> WeekDictExtension = new Dictionary<string, int>()
        {
            { " Monday", 1},
            { " Tuesday", 2},
            { " Wednesday", 3},
            { " Thursday", 4},
            { " Friday", 5},
            { " Saturday", 6},
            { " Sunday", 7},
        };

        public static Dictionary<int, string> intToMonth = new Dictionary<int, string>()
        {
            { 1, "January"},
            { 2, "February"},
            { 3, "March"},
            { 4, "April"},
            { 5, "May"},
            { 6, "June"},
            { 7, "July"},
            { 8, "August"},
            { 9, "September"},
            { 10, "October"},
            { 11, "November"},
            { 12, "December"},
        };

        static Globals()
        {
            DateTime now = DateTime.Now;
            string[] subs = now.GetDateTimeFormats().First().Split('.');
            date = new DateTime(int.Parse(subs[2]), int.Parse(subs[1]), 1, new GregorianCalendar());
            dateNow = new DateTime(int.Parse(subs[2]), int.Parse(subs[1]), int.Parse(subs[0]), new GregorianCalendar());
            calendar = CultureInfo.InvariantCulture.Calendar;
            numWeek = 1;
            numDayinWeek = 1;
            przestepny = false;
            isEnglishLanguage = true;

            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;" +
                "pwd=1234;database=kalendarz";

            conn = new MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();

            findTaskId();
            //generateLatte();
        }

        public static void generateLatte()
        {
            //for (int i = 2023; i < 2030; i++)
            //{
            //    int i = 2025;
            //    przestepny = i % 4 == 0;

            //    Dictionary<int, int> MonthDict = new Dictionary<int, int>()
            //    {
            //        {1,31},
            //        {2, przestepny ? 29 : 28},
            //        {3,31},
            //        {4,30},
            //        {5,31},
            //        {6,30},
            //        {7,31},
            //        {8,31},
            //        {9,30},
            //        {10,31},
            //        {11,30},
            //        {12,31},
            //    };

            //    for (int j = 1; j < 13; j++)
            //    {
            //        //sql = $"INSERT INTO kalendarz.miesiąc VALUES({j}, {i});";
            //        //command = new MySqlCommand(sql, conn);
            //        //reader = command.ExecuteReader();

            //        //reader.Close();

            //        for (int k = 1; k < MonthDict[j] + 1; k++)
            //        {
            //            sql = $"INSERT INTO kalendarz.dzień VALUES({k}, {j}, {i});";
            //            command = new MySqlCommand(sql, conn);
            //            reader = command.ExecuteReader();

            //            reader.Close();
            //        }
            //    }
            //}

            //for (int i = 2023; i < 2025; i++)
            //{
            //    for (int j = 1; j < 5; j++)
            //    {
            //        for (int k = 1; k < 7; k++)
            //        {
            //            sql = $"INSERT INTO kalendarz.tydzień VALUES({k}, {j}, {i});";
            //            command = new MySqlCommand(sql, conn);
            //            reader = command.ExecuteReader();

            //            reader.Close();
            //            for (int l = 1; l < 8; l++)
            //            {
            //                sql = $"INSERT INTO kalendarz.dzień_tygodnia VALUES({l}, {k}, {j}, {i});";
            //                command = new MySqlCommand(sql, conn);
            //                reader = command.ExecuteReader();

            //                reader.Close();
            //            }
            //        }
            //    }
            //}
        }

        public static void findTaskId()
        {
            sql = "SELECT * FROM kalendarz.zadanie;";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                taskId = reader.GetInt32(0);
            }
            reader.Close();
        }

        public static void dayTasks()
        {
            int indexTask = 0, indexHabit = 0;
            sql = $"call kalendarz.zadania_dnia_d({calendar.GetDayOfMonth(date)}, {calendar.GetMonth(date)}, {calendar.GetYear(date)});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            WeeklyDay.Instance.DayTasksListBox.Items.Clear();
            WeeklyDay.Instance.dayHabitsListBox.Items.Clear();

            Output = "";
            string notes = "";

            while (reader.Read())
            {
                if (reader.GetValue(6).ToString() == "")
                {
                    Output = reader.GetValue(8) + ". " + reader.GetValue(0) + ", " + reader.GetValue(2) + "\n";
                }
                else
                {
                    Output = reader.GetValue(8) + ". " + reader.GetValue(0) + ", "
                    + reader.GetValue(6).ToString().Remove(5) + "-" + reader.GetValue(7).ToString().Remove(5) + ", "
                    + reader.GetValue(2) + "\n";
                }

                if (reader.GetValue(9).ToString() != "")
                    notes = notes + reader.GetValue(8) + ". " + reader.GetValue(9) + "\n";

                if (bool.Parse(reader.GetValue(12).ToString()) == true)
                {
                    CheckboxElement checkboxElement = new CheckboxElement(indexHabit, Output, bool.Parse(reader.GetValue(10).ToString()));
                    WeeklyDay.Instance.dayHabitsListBox.Items.Add(checkboxElement);
                    indexHabit++;
                }
                else
                {
                    CheckboxElement checkboxElement = new CheckboxElement(indexTask, Output, bool.Parse(reader.GetValue(10).ToString()));
                    WeeklyDay.Instance.DayTasksListBox.Items.Add(checkboxElement);
                    indexTask++;
                }
            }
            WeeklyView.Instance.notesTextBox.Text = notes;
            reader.Close();
        }

        public static string weekTasks()
        {
            int i = 0;
            sql = $"call kalendarz.zadania_tygodnia_d({numWeek}, {calendar.GetMonth(date)}, {calendar.GetYear(date)});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            WeeklyTasks.Instance.weeklyTasksListBox.Items.Clear();
            Console.WriteLine(sql);
            Output = "";

            while (reader.Read())
            {
                Output = reader.GetValue(6) + ". " + reader.GetValue(0) + ", " + reader.GetValue(2) + "\n";

                CheckboxElement checkboxElement = new CheckboxElement(i, Output, bool.Parse(reader.GetValue(8).ToString()));
                WeeklyTasks.Instance.weeklyTasksListBox.Items.Add(checkboxElement);

                i++;
            }
            reader.Close();

            return Output;
        }

        public static string monthTasks()
        {
            int i = 0;
            sql = $"call kalendarz.zadania_miesiaca_d({calendar.GetMonth(date)}, {calendar.GetYear(date)});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            MonthlyTasks.Instance.monthlyTasksListBox.Items.Clear();

            Output = "";

            while (reader.Read())
            {
                Output = reader.GetValue(5) + ". " + reader.GetValue(0) + ", " + reader.GetValue(2) + "\n";

                CheckboxElement checkboxElement = new CheckboxElement(i, Output, bool.Parse(reader.GetValue(7).ToString()));
                MonthlyTasks.Instance.monthlyTasksListBox.Items.Add(checkboxElement);

                i++;
            }
            reader.Close();

            return Output;
        }

        public static void dayTasks(int whereToWrite, int eachDay)
        {
            int i = 0;
            sql = $"call kalendarz.zadania_dnia_d({eachDay}, {calendar.GetMonth(date)}, {calendar.GetYear(date)});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            Output = "";

            while (reader.Read())
            {
                if (reader.GetValue(6).ToString() == "")
                {
                    Output = reader.GetValue(8) + ". " + reader.GetValue(0) + ", " + reader.GetValue(2);
                }
                else
                {
                    Output = reader.GetValue(8) + ". " + reader.GetValue(0) + ", "
                    + reader.GetValue(6).ToString().Remove(5) + "-" + reader.GetValue(7).ToString().Remove(5) + ", "
                    + reader.GetValue(2);
                }
                WeeklyView.weekListBoxes[whereToWrite].Items.Add(Output);
                i++;
            }
            reader.Close();
        }

        public static void addTaskDay(string name, int daily, int habit, string category, string from, string to, string periodicity, string num, string notes)
        {
            bool flag = true;
            int categoryId = -1;
            int tempId = 0;
            int tempTaskId = 0;
            int periodicityInt;
            int dayNumInt;

            if (!isPastDay())
            {
                bool przestepny = calendar.GetYear(date) % 4 == 0;

                Dictionary<int, int> MonthDict = new Dictionary<int, int>()
                {
                    {1,31},
                    {2, przestepny ? 29 : 28},
                    {3,31},
                    {4,30},
                    {5,31},
                    {6,30},
                    {7,31},
                    {8,31},
                    {9,30},
                    {10,31},
                    {11,30},
                    {12,31},
                };

                //Sprawdzamy kategorię
                sql = $"select* from kalendarz.kategoria";
                command = new MySqlCommand(sql, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(1).ToString() == category)
                    {
                        flag = false;
                        categoryId = int.Parse(reader.GetValue(0).ToString());
                    }
                    tempId = int.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();

                //Jeżeli kategoria nie istnieje
                if (flag)
                {
                    sql = $"call kalendarz.insert_kategoria('{category}');";
                    askDatabase();

                    categoryId = tempId + 1;
                }

                tempTaskId = taskId + 1;

                //Dodanie do zadań
                if (habit == 0)
                {
                    sql = $"call kalendarz.insert_zadanie('{name}', {daily}, 0, 0, '{notes}', {habit}, null, null, null, 'Maciek', {categoryId});";
                    askDatabase();

                    taskId++;
                }
                else if (habit == 1)
                {
                    periodicityInt = int.Parse(periodicity);
                    dayNumInt = int.Parse(num);

                    sql = $"call kalendarz.insert_zadanie('{name}', {daily}, 0, 0, '{notes}', {habit}, null, {periodicityInt}, {dayNumInt}, 'Maciek', {categoryId});";
                    askDatabase();

                    taskId++;

                    for (int i = 1, checkDays = calendar.GetDayOfMonth(date) + periodicityInt; i < dayNumInt && checkDays < MonthDict[calendar.GetMonth(date)] + 1; i++, checkDays += periodicityInt)
                    {
                        sql = $"call kalendarz.insert_zadanie('{name}', {daily}, 0, 0, '{notes}', {habit}, null, {periodicityInt}, 1, 'Maciek', {categoryId});";
                        askDatabase();

                        taskId++;
                    }
                }

                //Dodanie do zadań dnia
                if (habit == 0)
                {
                    if (daily == 0)
                    {
                        sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({calendar.GetDayOfMonth(date)}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', '{from}', '{to}', {taskId});";
                    }
                    else if (daily == 1)
                    {
                        sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({calendar.GetDayOfMonth(date)}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', null, null, {taskId});";
                    }
                    askDatabase();
                }
                else if (habit == 1)
                {
                    periodicityInt = int.Parse(periodicity);
                    dayNumInt = int.Parse(num);

                    if (daily == 0)
                    {
                        sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({calendar.GetDayOfMonth(date)}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', '{from}', '{to}', {tempTaskId});";

                        askDatabase();

                        for (int i = 1, checkDays = calendar.GetDayOfMonth(date) + periodicityInt; i < dayNumInt && checkDays < MonthDict[calendar.GetMonth(date)] + 1; i++, checkDays += periodicityInt)
                        {
                            tempTaskId++;
                            sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({checkDays}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', '{from}', '{to}', {tempTaskId});";

                            askDatabase();
                        }
                    }
                    else if (daily == 1)
                    {
                        sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({calendar.GetDayOfMonth(date)}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', null, null, {tempTaskId});";

                        askDatabase();

                        for (int i = 1, checkDays = calendar.GetDayOfMonth(date) + periodicityInt; i < dayNumInt && checkDays < MonthDict[calendar.GetMonth(date)] + 1; i++, checkDays += periodicityInt)
                        {
                            tempTaskId++;
                            sql = $"call kalendarz.insert_zadanie_into_zadania_dnia({checkDays}" +
                            $", {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', null, null, {tempTaskId});";

                            askDatabase();
                        }
                    }
                }
                generateWeekTitle();
            }
        }

        public static void addTaskWeek(string name, string category, string notes)
        {
            bool flag = true;
            int categoryId = -1;
            int tempId = 0;

            //Sprawdzamy kategorię
            sql = $"select* from kalendarz.kategoria";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetValue(1).ToString() == category)
                {
                    flag = false;
                    categoryId = int.Parse(reader.GetValue(0).ToString());
                }
                tempId = int.Parse(reader.GetValue(0).ToString());
            }
            reader.Close();

            //Jeżeli kategoria nie istnieje
            if (flag)
            {
                sql = $"call kalendarz.insert_kategoria('{category}');";
                askDatabase();

                categoryId = tempId + 1;
            }

            //Dodanie do zadań
            sql = $"call kalendarz.insert_zadanie('{name}', 1, 0, 0, '{notes}', 0, null, null, null, 'Maciek', {categoryId});";
            askDatabase();

            taskId++;

            //Dodanie do zadań miesiąca
            sql = $"call kalendarz.insert_zadanie_into_zadanie_tygodnia({numWeek}, {calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', {taskId});";
            askDatabase();
        }

        public static void addTaskMonth(string name, string category, string notes)
        {
            bool flag = true;
            int categoryId = -1;
            int tempId = 0;

            if (!isPastMonth())
            {
                //Sprawdzamy kategorię
                sql = $"select* from kalendarz.kategoria";
                command = new MySqlCommand(sql, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(1).ToString() == category)
                    {
                        flag = false;
                        categoryId = int.Parse(reader.GetValue(0).ToString());
                    }
                    tempId = int.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();

                //Jeżeli kategoria nie istnieje
                if (flag)
                {
                    sql = $"call kalendarz.insert_kategoria('{category}');";
                    askDatabase();

                    categoryId = tempId + 1;
                }

                //Dodanie do zadań
                sql = $"call kalendarz.insert_zadanie('{name}', 1, 0, 0, '{notes}', 0, null, null, null, 'Maciek', {categoryId});";
                askDatabase();

                taskId++;

                //Dodanie do zadań miesiąca
                sql = $"call kalendarz.insert_zadanie_into_zadanie_miesiąca({calendar.GetMonth(date)}, {calendar.GetYear(date)}, 'Maciek', {taskId});";
                askDatabase();
            }
        }

        public static void modifyTaskDay(string id, string name, string category, string from, string to, string notes)
        {
            bool flag = true;
            int categoryId = -1;
            int tempId = 0;
            bool daily;

            if (!checkArchive(id))
            {
                //Czytamy dane o zadaniu
                sql = $"call kalendarz.dane_zadanie_id({id});";
                command = new MySqlCommand(sql, conn);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    daily = bool.Parse(reader.GetValue(2).ToString());
                }
                else
                {
                    return;
                }
                reader.Close();

                //Sprawdzamy kategorię
                sql = $"select* from kalendarz.kategoria";
                command = new MySqlCommand(sql, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(1).ToString() == category)
                    {
                        flag = false;
                        categoryId = int.Parse(reader.GetValue(0).ToString());
                    }
                    tempId = int.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();

                //Jeżeli kategoria nie istnieje
                if (flag)
                {
                    sql = $"call kalendarz.insert_kategoria('{category}');";
                    askDatabase();

                    categoryId = tempId + 1;
                }

                //Zmiana w zadaniach
                sql = $"call kalendarz.update_zadanie({id}, '{name}', '{notes}', {categoryId});";
                askDatabase();

                //Zmiana zadań dnia
                if (!daily)
                {
                    sql = $"call kalendarz.update_zadanie_dnia('{from}', '{to}', {id});";
                }
                else
                {
                    sql = $"call kalendarz.update_zadanie_dnia(null, null, {id});";
                }
                askDatabase();
            }
        }

        public static void modifyTaskMonth(string id, string name, string category, string notes)
        {
            bool flag = true;
            int categoryId = -1;
            int tempId = 0;

            if (!checkArchive(id))
            {
                //Sprawdzamy kategorię
                sql = $"select* from kalendarz.kategoria";
                command = new MySqlCommand(sql, conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetValue(1).ToString() == category)
                    {
                        flag = false;
                        categoryId = int.Parse(reader.GetValue(0).ToString());
                    }
                    tempId = int.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();

                //Jeżeli kategoria nie istnieje
                if (flag)
                {
                    sql = $"call kalendarz.insert_kategoria('{category}');";
                    askDatabase();

                    categoryId = tempId + 1;
                }

                //Zmiana w zadaniach
                sql = $"call kalendarz.update_zadanie({id}, '{name}', '{notes}', {categoryId});";
                askDatabase();
            }
        }

        public static void modifyDone(string taskId, int isDone)
        {
            if (!checkArchive(taskId))
            {
                sql = $"call kalendarz.update_zadanie_czywykonane({taskId}, {isDone});";
                askDatabase();
            }
        }
        public static void deleteTaskDay(string taskId)
        {
            if (!checkArchive(taskId))
            {
                sql = $"call kalendarz.delete_from_zadanie_id({int.Parse(taskId)});";
                askDatabase();
            }
        }

        public static bool checkArchive(string taskId)
        {
            bool isArchive = false;

            sql = $"call kalendarz.dane_zadanie_id({int.Parse(taskId)});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            reader.Read();
            isArchive = bool.Parse(reader.GetValue(4).ToString());
            reader.Close();

            return isArchive;
        }

        public static bool isPastMonth()
        {
            bool Output = false;

            if (calendar.GetYear(date) < calendar.GetYear(dateNow))
            {
                Output = true;
            }
            else if (calendar.GetYear(date) == calendar.GetYear(dateNow))
            {
                if (calendar.GetMonth(date) < calendar.GetMonth(dateNow))
                {
                    Output = true;
                }
            }

            return Output;
        }

        public static bool isPastWeek()
        {
            bool Output = false;

            if (calendar.GetYear(date) < calendar.GetYear(dateNow))
            {
                Output = true;
            }
            else if (calendar.GetYear(date) == calendar.GetYear(dateNow))
            {
                if (calendar.GetMonth(date) < calendar.GetMonth(dateNow))
                {
                    Output = true;
                }
                else if (calendar.GetMonth(date) == calendar.GetMonth(dateNow))
                {
                    //if (numWeek < calendar.g (dateNow))
                    //{
                    //    Output = true;
                    //}
                }
            }

            return Output;
        }

        public static bool isPastDay()
        {
            bool Output = false;

            if (calendar.GetYear(date) < calendar.GetYear(dateNow))
            {
                Output = true;
            }
            else if (calendar.GetYear(date) == calendar.GetYear(dateNow))
            {
                if (calendar.GetMonth(date) < calendar.GetMonth(dateNow))
                {
                    Output = true;
                }
                else if (calendar.GetMonth(date) == calendar.GetMonth(dateNow))
                {
                    if (calendar.GetDayOfMonth(date) < calendar.GetDayOfMonth(dateNow))
                    {
                        Output = true;
                    }
                }
            }

            return Output;
        }

        public static string dayString()
        {
            return calendar.GetDayOfMonth(date) + "." + calendar.GetMonth(date) + "." + calendar.GetYear(date);
        }

        public static string monthString()
        {
            return calendar.GetMonth(date) + "." + calendar.GetYear(date);
        }

        public static void chooseMonth(int whichMonth)
        {
            date = new DateTime(calendar.GetYear(date), whichMonth, 1, new GregorianCalendar());
        }

        public static void chooseWeek(int whichWeek)
        {
            numWeek = whichWeek;
        }

        public static void chooseDay(int whichDay)
        {
            date = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), whichDay, new GregorianCalendar());
        }

        public static void addMonth()
        {
            date = calendar.AddMonths(date, 1);
        }

        public static void subMonth()
        {
            date = calendar.AddMonths(date, -1);
        }

        public static void addYear()
        {
            date = calendar.AddYears(date, 1);
        }

        public static void subYear()
        {
            date = calendar.AddYears(date, -1);
        }
        public static void subWeek()
        {
            if (numWeek > 1)
            {
                numWeek -= 1;
            }
        }
        public static void addWeek()
        {
            if (numWeek < 5)
            {
                numWeek += 1;
            }
        }

        public static void askDatabase()
        {
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            reader.Close();
        }

        public static void getMonthOrWeekData(string id)
        {
            string tempCat = "";
            sql = $"call kalendarz.dane_zadanie_id({id});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            reader.Read();
            ModifyTaskWindow.Instance.nameTextBlock.Text = $"{reader.GetValue(1)}";
            tempCat = $"{reader.GetValue(11)}";
            ModifyTaskWindow.Instance.notesTextBlock.Text = $"{reader.GetValue(5)}";
            reader.Close();

            //Sprawdzamy kategorię
            sql = $"select* from kalendarz.kategoria";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0).ToString());
                Console.WriteLine(id);
                if (reader.GetValue(0).ToString() == tempCat)
                {
                    tempCat = reader.GetValue(1).ToString();
                }
            }
            reader.Close();

            ModifyTaskWindow.Instance.categoryTextBlock.Text = tempCat;
        }

        public static void getDayData(string id)
        {
            string tempCat = "";
            sql = $"call kalendarz.dane_zadanie_dnia_id({id});";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            reader.Read();
            ModifyTaskWindow.Instance.nameTextBlock.Text = $"{reader.GetValue(1)}";
            tempCat = $"{reader.GetValue(11)}";
            ModifyTaskWindow.Instance.notesTextBlock.Text = $"{reader.GetValue(5)}";
            ModifyTaskWindow.Instance.hoursFromTextBlock.Text = $"{reader.GetValue(12)}";
            ModifyTaskWindow.Instance.hoursToTextBlock.Text = $"{reader.GetValue(13)}";
            reader.Close();

            //Sprawdzamy kategorię
            sql = $"select* from kalendarz.kategoria";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0).ToString());
                Console.WriteLine(id);
                if (reader.GetValue(0).ToString() == tempCat)
                {
                    tempCat = reader.GetValue(1).ToString();
                }
            }
            reader.Close();

            ModifyTaskWindow.Instance.categoryTextBlock.Text = tempCat;
        }

        public static void generateWeekTitle()
        {
            int firstDay;
            int lastDay;
            przestepny = calendar.GetYear(date) % 4 == 0;

            Dictionary<int, int> MonthDict = new Dictionary<int, int>()
            {
                {1,31},
                {2, przestepny ? 29 : 28},
                {3,31},
                {4,30},
                {5,31},
                {6,30},
                {7,31},
                {8,31},
                {9,30},
                {10,31},
                {11,30},
                {12,31},
            };

            for (int i = 1; i < WeeklyView.weekListBoxes.Count + 1; i++)
            {
                WeeklyView.weekListBoxes[i].Items.Clear();
            }

            chooseDay(1);
            string temp = calendar.GetDayOfWeek(date).ToString();
            weekTasks();

            int leftDays = 8 - WeekDict[temp];
            if (numWeek == 1)
            {
                numDayinWeek = leftDays - 1;
                if (leftDays == 1)
                {
                    MainWindow.Instance.Title.Text = 1.ToString() + "." + calendar.GetMonth(date);
                    dayTasks(7, 1);
                }
                else
                {
                    MainWindow.Instance.Title.Text = 1.ToString() + "." + calendar.GetMonth(date) + "-"
                                                        + leftDays + "." + calendar.GetMonth(date);

                    for (int i = WeekDict[temp], eachDay = 1; i < WeeklyView.weekListBoxes.Count + 1; i++, eachDay++)
                    {
                        dayTasks(i, eachDay);
                    }
                }
            }
            else if (numWeek == 2)
            {
                numDayinWeek = leftDays;
                firstDay = leftDays + 1;
                lastDay = leftDays + 7;
                MainWindow.Instance.Title.Text = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);

                for (int i = 1, eachDay = firstDay; i < WeeklyView.weekListBoxes.Count + 1; i++, eachDay++)
                {
                    dayTasks(i, eachDay);
                }
            }
            else if (numWeek == 3)
            {
                numDayinWeek = leftDays + 7;
                firstDay = leftDays + 8;
                lastDay = leftDays + 14;
                MainWindow.Instance.Title.Text = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);

                for (int i = 1, eachDay = firstDay; i < WeeklyView.weekListBoxes.Count + 1; i++, eachDay++)
                {
                    dayTasks(i, eachDay);
                }
            }
            else if (numWeek == 4)
            {
                numDayinWeek = leftDays + 14;
                firstDay = leftDays + 15;
                lastDay = leftDays + 21;
                MainWindow.Instance.Title.Text = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);

                for (int i = 1, eachDay = firstDay; i < WeeklyView.weekListBoxes.Count + 1; i++, eachDay++)
                {
                    dayTasks(i, eachDay);
                }
            }
            else if (numWeek == 5)
            {
                numDayinWeek = leftDays + 21;
                firstDay = leftDays + 22;
                lastDay = leftDays + 28;
                if (firstDay < 25)
                {
                    MainWindow.Instance.Title.Text = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);

                    for (int i = 1, eachDay = firstDay; i < WeeklyView.weekListBoxes.Count + 1; i++, eachDay++)
                    {
                        dayTasks(i, eachDay);
                    }
                }
                else
                {
                    MainWindow.Instance.Title.Text = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + MonthDict[calendar.GetMonth(date)] + "." + calendar.GetMonth(date);

                    for (int i = 1, eachDay = firstDay; eachDay < MonthDict[calendar.GetMonth(date)] + 1; i++, eachDay++)
                    {
                        dayTasks(i, eachDay);
                    }
                }

            }
        }

        public static string weekString()
        {
            int firstDay;
            int lastDay;
            string weekTitle = "";
            przestepny = calendar.GetYear(date) % 4 == 0;

            Dictionary<int, int> MonthDict = new Dictionary<int, int>()
            {
                {1,31},
                {2, przestepny ? 29 : 28},
                {3,31},
                {4,30},
                {5,31},
                {6,30},
                {7,31},
                {8,31},
                {9,30},
                {10,31},
                {11,30},
                {12,31},
            };

            chooseDay(1);
            string temp = calendar.GetDayOfWeek(date).ToString();
            Console.WriteLine(temp);

            int leftDays = 8 - WeekDict[temp];
            if (numWeek == 1)
            {
                numDayinWeek = leftDays - 1;
                if (leftDays == 1)
                {
                    weekTitle = 1.ToString() + "." + calendar.GetMonth(date);
                }
                else
                {
                    weekTitle = 1.ToString() + "." + calendar.GetMonth(date) + "-"
                                                        + leftDays + "." + calendar.GetMonth(date);
                }
            }
            else if (numWeek == 2)
            {
                numDayinWeek = leftDays;
                firstDay = leftDays + 1;
                lastDay = leftDays + 7;
                weekTitle = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);
            }
            else if (numWeek == 3)
            {
                numDayinWeek = leftDays + 7;
                firstDay = leftDays + 8;
                lastDay = leftDays + 14;
                weekTitle = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);
            }
            else if (numWeek == 4)
            {
                numDayinWeek = leftDays + 14;
                firstDay = leftDays + 15;
                lastDay = leftDays + 21;
                weekTitle = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);
            }
            else if (numWeek == 5)
            {
                numDayinWeek = leftDays + 21;
                firstDay = leftDays + 22;
                lastDay = leftDays + 28;
                if (firstDay < 25)
                {
                    weekTitle = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + lastDay + "." + calendar.GetMonth(date);
                }
                else
                {
                    weekTitle = firstDay + "." + calendar.GetMonth(date) + "-"
                                                    + MonthDict[calendar.GetMonth(date)] + "." + calendar.GetMonth(date);
                }
            }

            weekTitle = weekTitle + "." + calendar.GetYear(date);
            return weekTitle;
        }
    }
}
