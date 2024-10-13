using Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public abstract class DTOModel : PropChanged
    {
        public abstract object? ToDto(object? objectLoader);   
    }
}
