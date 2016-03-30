using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Media;

namespace Pin.Model
{
    public class Project
    {
        List<Item> StandardList;
        StringCollection StandardPropertyCollection = new StringCollection();
        private bool changeDetected = false;
        int idCounter = 0;

        public Project(StringCollection savePropertyCollection) : this()
        {
            load(savePropertyCollection);
        }

        public Project()
        {
            StandardList = new List<Item>();
        }

        public void add(Color color, string Name, string Directory, bool isFavorite, bool isPinned, Item.Operation operations)
        {
            StandardList.Add(new Project.Item(idCounter++,color,Name,Directory,isFavorite,isPinned,operations));
            changeDetected = true;
        }

        public void add(int id,String loadString)
        {
            StandardList.Add(new Project.Item(loadString));
            changeDetected = true;
        }

        public void load()
        {
            load(StandardPropertyCollection);
        }
        public void load(StringCollection savePropertyCollection)
        {
            foreach (string item in savePropertyCollection)
            {
                add(idCounter++,item);
            }
        }

        public void save(StringCollection savePropertyCollection)
        {
            //foreach (Item item in StandardList)
                //if (item.changeDetected && StandardPropertyCollection.(item.id))
                //{
                    
                //}

                foreach (Item item in StandardList)
                StandardPropertyCollection.Add(item.saveValue());
            //Settings.saveProjects(StandardPropertyCollection);
            
        }
        public class Item
        {
            #region Declarations
            public readonly int id;
            private Color itemColor;
            public Color ItemColor
            {
                get
                {
                    return itemColor;
                }
                set
                {
                    changeDetected = true;
                    ItemColor = value;
                }
            }
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    changeDetected = true;
                    name = value;
                }
            }
            private string directory;
            public string Directory
            {
                get
                {
                    return directory;
                }
                set
                {
                    changeDetected = true;
                    directory = value;
                }
            }
            private bool isFavorite;
            public bool IsFavorite
            {
                get
                {
                    return isFavorite;
                }
                set
                {
                    changeDetected = true;
                    isFavorite = value;
                }
            }
            private bool isPinned;
            public bool IsPinned
            {
                get
                {
                    return isPinned;
                }
                set
                {
                    changeDetected = true;
                    isPinned = value;
                }
            }
            public static char separator = (char)31; // INFORMATION SEPARATOR ONE
            private Operation operation;
            public Operation Operations
            {
                get
                {
                    return operation;
                }
                set
                {
                    changeDetected = true;
                    operation = value;
                }
            }
            public bool changeDetected = false;
            #endregion
            public enum Operation
            {
                Move,
                Copy
            }
            public Item()
            {

            }

            /// <summary>
            /// Loads project item from string
            /// </summary>
            /// <param name="loadString"></param>
            public Item(String loadString)
            {
                // parse the project item string
                String[] properties = loadString.Split(separator);
                try
                {
                    int i = 0;
                    itemColor = (Color)ColorConverter.ConvertFromString(properties[i++]);
                    Name = properties[i++];
                    Directory = properties[i++];
                    isFavorite = Boolean.Parse(properties[i++]);
                    isPinned = Boolean.Parse(properties[i++]);
                    operation = ParseEnum<Operation>(properties[i++]);
                }
                catch (Exception)
                {
                    // problem with properties
                }

            }

            public Item(int id, Color color, string Name, string Directory, bool isFavorite, bool isPinned, Operation operations)
            {
                this.id = id;
                this.itemColor = color;
                this.Name = Name;
                this.Directory = Directory;
                this.isFavorite = isFavorite;
                this.isPinned = isPinned;
                this.operation = operations;
            }

            public override string ToString()
            {
                return itemColor.ToString() + separator
                     + Name + separator
                     + Directory + separator
                     + isFavorite + separator
                     + isPinned;
            }
            public string saveValue()
            {
                return ToString();
            }
            public static T ParseEnum<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
        }
    }




}
