using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWork_V2._0
{
    struct Worker
    {
        private int iD;
        public DateTime TimeOfAdd;
        private string fIO;
        private byte age;
        private int height;
        private DateTime dateOfBirth;
        private string placeOfBorn;
        public int ID
        {
            get { return this.iD; }
            set { this.iD = value; }
        }

        public string FIO
        {
            get { return fIO; }
            set { this.fIO = value; }
        }
        public byte Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }
        public string PlaceOfBorn
        {
            get { return this.placeOfBorn; }
            set { this.placeOfBorn = value;}
        }

        public Worker(int ID, DateTime TimeOfAdd, string FIO, byte Age, int Height, DateTime DateOfBirth, string PlaceOfBorn)
        {
            this.iD = ID;
            this.TimeOfAdd = TimeOfAdd;
            this.fIO = FIO;
            this.age = Age;
            this.height = Height;
            this.dateOfBirth = DateOfBirth;
            this.placeOfBorn = PlaceOfBorn;
            
        }
    }
}
