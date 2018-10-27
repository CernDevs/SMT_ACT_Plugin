using SMT.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.GoogleDoc
{
    public static class ReaderStaticMembers
    {
        public static List<CharacterNameDTO> GetStaticMembers()
        {
            //TODO Stuff to get character names
            //Make into Array
            //return
            //for now: Mock-Static-List
            return new List<CharacterNameDTO>()
            {
                new CharacterNameDTO("Driez", "Dragonstorm"),
                new CharacterNameDTO("Zetsubo", "Nokage"),
                new CharacterNameDTO("Axel", "Astronix"),
                new CharacterNameDTO("Lulufi", "Lufi"),
                new CharacterNameDTO("Choco", "Bun"),
                new CharacterNameDTO("Professor", "Ari"),
                new CharacterNameDTO("Nhagi'ir", "Lyegah"),
                new CharacterNameDTO("Thiniker", "Vilauclaire"),
            };
        }
    }
}
