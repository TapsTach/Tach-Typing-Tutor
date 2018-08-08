using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TawmFramework
{

    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        object Content { get; set; }

        //Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
        void Hide();
        void Show();
    }
    
  
}
