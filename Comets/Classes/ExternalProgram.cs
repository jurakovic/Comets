
namespace Comets.Classes
{
    public class ExternalProgram
    {
        #region Properties

        public int Type; // { get; set; } // nije property da se ne binda u gridu u settingsima
        public string Directory { get; set; }
        public string Name
        {
            get
            {
                return ElementTypes.TypeName[Type];
            }
        }

        #endregion

        #region Constructor

        public ExternalProgram(int type, string directory)
        {
            this.Type = type;
            this.Directory = directory;
        }

        #endregion
    }
}
