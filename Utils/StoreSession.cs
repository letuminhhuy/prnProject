using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Utils
{
    public class StoreSession
    {
        private static StoreSession _instance;
        public static StoreSession Instance { get
            {
                if(_instance == null)
                {
                    _instance = new StoreSession();
                }
                return _instance;
            }
        }

        public Employee? ActiveEmployee { get; set; }

        public StoreSession() { }

        public void SetActiveEmployee(Employee emp)
        {
            ActiveEmployee = emp;
        }
    }
}
