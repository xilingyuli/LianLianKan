using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 连连看1._0
{
    class aButton : Button
    {
        public int x,y;
	    public aButton(int i,int j) : base()
	    {
		    x = i;
		    y = j;
	    }
    }
}
