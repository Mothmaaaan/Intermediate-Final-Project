using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ClassList : MonoBehaviour
{
    // Classes
    public Class Magician = new Class("Magician", 100, 200);
    public Class StreetPerformer = new Class("Street Performer", 120, 180);
    public Class EscapeArtist = new Class("Escape Artist", 80, 240);
    public Class TestDummy = new Class("Test Dummy", 5, 400);

    // Class List
    public List<Class> classes = new List<Class>();
    

    private void Awake() {
        classes.Add(Magician);
        classes.Add(StreetPerformer);
        classes.Add(EscapeArtist);
        classes.Add(TestDummy);
    }

#region Add Class
    // Return the class that has the same name as className.
    public Class SearchForClass(string className){
        
        for(int i=0; i<classes.Count; i++){
            if(classes[i].name == className)
                return classes[i];
        }

        // Return the first class if we cannot find what class we are searching for.
        return classes[0];
    }
#endregion

#region Saving and Loading Classes
// Save the class list to an XML file.
// VISUAL STUDIO HAS ME RUN A COMMAND TO FORMAL IN A READABLE FORMAT.
    public void SaveClasses(){
        XmlWriter writer = XmlWriter.Create("Assets/ClassList");
        writer.WriteStartDocument();

        writer.WriteStartElement("ClassList");
        foreach(Class c in classes){
            writer.WriteStartElement("Class");
            writer.WriteElementString("Name", c.name);
            writer.WriteElementString("Health", c.health.ToString());
            writer.WriteElementString("Speed", c.speed.ToString());
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.Close();
    }

// Load our classes from the XML file.

#endregion
}
