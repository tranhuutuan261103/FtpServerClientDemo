using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.UI.Fonts
{
    public class InterFont
    {
        private PrivateFontCollection pfc = new PrivateFontCollection();
        public InterFont()
        {
            pfc.AddFontFile("../../../UI/Fonts/InterFonts/Inter-Black.ttf");
            pfc.AddFontFile("../../../UI/Fonts/InterFonts/Inter-Bold.ttf");

            pfc.AddFontFile("../../../UI/Fonts/InterFonts/Inter-Regular.ttf");
        }

        public Font InterBlack(float size)
        {
            return new Font(pfc.Families[0], size);
        }

        public Font InterBold(float size)
        {
            return new Font(pfc.Families[1], size);
        }

        public Font InterRegular(float size)
        {
            return new Font(pfc.Families[2], size);
        }
    }
}
