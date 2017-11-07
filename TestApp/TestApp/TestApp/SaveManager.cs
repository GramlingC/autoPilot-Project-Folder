﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading.Tasks;
//using System.Runtime.Serialization;

// Binary serialization seems to not be supported by Xamarin, so 
// we can use XML serialization for now until we find an alternative solution.

namespace TestApp
{
    // The SaveManager class can write objects to files and read
    // them back by the use of object serialization.
    public class SaveManager
    {
        // Completed, need testing

        public static async void SaveObject(string fileName, object obj)
        {           

            // If the file exists, we delete it so we can rewrite our new file.
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // The using block lets us open a FileStream safely, because when 
            // it closes, it will automatically close the stream for us. As we
            // open the filestream, we create our save file.
            using (FileStream stream = Task.Run(() => File.Create(fileName)).Result)
            {
                // Use an XML formatter to format to xml data
                // Note: XmlSerializer MUST be instantiated with a type in the constructor
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                try
                {
                    await Task.Run(() => serializer.Serialize(stream, obj));
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error serializing object: " + e.Message);
                }
            }
        }
        public static async void SaveObjects(string fileName, params object[] objList)
        {
            //fileName = "../../../../../" + fileName;

            //bool wasSuccessful = true;

            // If the file exists, we delete it so we can rewrite our new file.
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // The using block lets us open a FileStream safely, because when 
            // it closes, it will automatically close the stream for us. As we
            // open the filestream, we create our save file.
            using (FileStream stream = Task.Run(() => File.Create(fileName)).Result)
            {
                // Convert each object into a stream of data, and write it to the file.
                for (int index = 0; index < objList.Length; index++)
                {
                    // Use an XML formatter to format to xml data
                    // Note: XmlSerializer MUST be instantiated with a type in the constructor,
                    // which is why it is created inside the for loop.
                    XmlSerializer serializer = new XmlSerializer(objList[index].GetType());

                    try
                    {
                        await Task.Run(() => serializer.Serialize(stream, objList[index]));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error serializing objects: " + e.Message);
                        //wasSuccessful = false;
                    }
                }
            }

            //stream.Close();

            //return wasSuccessful;
        }
        // LoadObject has the unique requirement that we cannot pass in a generic object with "ref".
        // Thus, we have to pass it back as a return value and assign it in the calling function.
        public static object LoadObject(string fileName, object obj)
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = Task.Run(() => File.OpenRead(fileName)).Result)
                {
                    // Use an XML formatter to format xml data
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());

                    try
                    {
                        obj = Task.Run(() => serializer.Deserialize(stream)).Result;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error deserializing objects: " + e.Message);
                    }
                }
            }
            else
            {
                Debug.WriteLine("Error Loading Objects: File does not exist.");
            }

            return obj;
        }
        // LoadObjects has a unique requirement in that we cannot use "params" and "out" or "ref" together.
        // Thus, we must return the object array back as a return value, meaning it must be parsed in the calling function.
        public static object[] LoadObjects(string fileName, params object[] objList)
        {            
            if (File.Exists(fileName))
            {
                using (FileStream stream = Task.Run(() => File.OpenRead(fileName)).Result)
                {
                    for (int index = 0; index < objList.Length; index++)
                    {
                        // Use an XML formatter to format xml data
                        XmlSerializer serializer = new XmlSerializer(objList[index].GetType());

                        try
                        {
                            objList[index] = Task.Run(() => serializer.Deserialize(stream)).Result;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Error deserializing objects: " + e.Message);
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Error Loading Objects: File does not exist.");
            }

            return objList;
        }

        // Not needed, here for historical purposes?
        /*
        bool SaveVariables(string fileName, params object[] atrList)
        {
            // We will assume everything goes well.  If not, we will return false through this.
            bool wasSuccessful = true;

            using (TextWriter writer = new StreamWriter(fileName))
            {
                foreach (object o in atrList)
                {
                    try
                    {
                        writer.WriteLine(o);
                    }
                    catch (SecurityException e)
                    {
                        Console.Write("Error serializing attributes: " + e.Message);
                        wasSuccessful = false;
                    }
                }
            }

            return wasSuccessful;
        }
        bool LoadVariables(string fileName, params object[] atrList)
        {
            // We will assume everything goes well.  If not, we will return false through this.
            bool wasSuccessful = true;

            using (TextReader reader = new StreamReader(fileName))
            {
                string input;
                foreach (object o in atrList)
                {
                    // Note - Not finished, does not read into objects
                    try
                    {
                        //if (o.GetType())
                        input = reader.ReadLine();
                        //o = Convert.
                    }
                    catch (SecurityException e)
                    {
                        Console.Write("Error serializing attributes: " + e.Message);
                        wasSuccessful = false;
                    }
                }
            }

            return wasSuccessful;
        }
        */
    }
}
