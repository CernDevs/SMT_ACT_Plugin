using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.DTO
{
    public class CharacterNameDTO
    {
        private string _firstName;
        private string _lastName;

        public string FirstName {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public CharacterNameDTO(string first, string last)
        {
            this.FirstName = first;
            this.LastName = last;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        

    }
}
