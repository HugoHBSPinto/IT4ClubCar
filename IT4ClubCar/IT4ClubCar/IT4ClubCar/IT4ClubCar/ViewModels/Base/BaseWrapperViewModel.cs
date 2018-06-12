using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace IT4ClubCar.IT4ClubCar.ViewModels.Base
{
    /// <summary>
    /// Classe base para viewmodels que fazem wrap de models.
    /// Herda do ExtendedBindableObject.
    /// </summary>
    class BaseWrapperViewModel : ExtendedBindableObject
    {
        public BaseWrapperViewModel()
        {
            
        }
    }
}
