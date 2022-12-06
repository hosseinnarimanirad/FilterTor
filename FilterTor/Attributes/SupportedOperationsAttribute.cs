﻿using System;
using System.Collections.Generic;
using System.Text;


namespace FilterTor.Attributes
{
    internal class SupportedOperationsAttribute : Attribute
    {
        public List<Operation> SupportedOperations { get; private set; }

        /// <summary>
        /// Defines operations that are supported by an specific TypeGroup.
        /// </summary>
        /// <param name="supportedOperations">List of supported operations.</param>
        public SupportedOperationsAttribute(params Operation[] supportedOperations)
        {
            SupportedOperations = new List<Operation>(supportedOperations);
        }
    }
}
