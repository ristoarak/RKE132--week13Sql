﻿
using Microsoft.VisualBasic;
using System.Data.SQLite;
ReadData(CreatConnection());
//InsertCustomer(CreatConnection());
RemoveCustomer(CreatConnection());


static SQLiteConnection CreatConnection()
{
    {
        SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True");
        try
        {
            connection.Open();
            Console.WriteLine("DB found");
        }
        catch
        {
            Console.WriteLine("DB not found!");
        }
        return connection;
    }
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read()) 
    {
        string readerRowId = reader["rowid"].ToString();
    string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; Status: {readerStringDoB}");
    }
    myConnection.Close();

}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name");
    fName = Console.ReadLine();
    Console.WriteLine("Enter lastname:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth ( mm-dd-yyyy): ");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{dob}')";
    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

  

    ReadData(myConnection);
    }
static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enteran id to dele a customer");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved}  was rmoved from the table customer.");

    ReadData(myConnection);
}