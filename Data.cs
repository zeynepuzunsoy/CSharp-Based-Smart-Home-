using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    internal class Data
    {
        public string Current_State { get; set; }  //hareket

        public string Lumen { get; set; }   //ldr
        public string Lumen2 { get; set; }

        public string Lumen3 { get; set; }
        public string Lamp { get; set; }  //ampul
        public string Lamp_Level{ get; set; }

        public string picture_url { get; set; } 

        public string Camera { get; set; }
        public string Lamp_Control { get; set; }
        public string RFID_Control { get; set; }    //rfid

        public string State { get; set; }  //door
        

    }
}
