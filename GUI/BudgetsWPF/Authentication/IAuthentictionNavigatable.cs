using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgets.GUI.WPF.Authentication
{
    public enum AuthNavigatableTypes
    {
        SignIn,
        SignUp
    }


    public interface IAuthNavigatable
    {
        public AuthNavigatableTypes Type { get; }

        public void ClearSensitiveData();
    }
}
