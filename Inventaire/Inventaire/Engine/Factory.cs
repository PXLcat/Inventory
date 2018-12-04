using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Factory //attention pas thread safe
    {
        private static Factory instance = null;
        public MainGame mG;

        public static Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                }
                return instance;
            }
        }

        public void SetMainGame(MainGame mG)
        {
            this.mG = mG;
        }


        private Factory()
        {
        }


    }

    public enum CharacterName
    {
        HUMAN,
        HULK,
        FOETUS,
        SPIRIT
    }
    public enum ForegroundItemName
    {
        BARREL
    }

    //public class FactoryDTO {

    //}
}
