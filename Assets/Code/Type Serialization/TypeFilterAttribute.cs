﻿using System;
using UnityEngine;

namespace Systems.TypeSerialization
{
    public class TypeFilterAttribute : PropertyAttribute
    {
        public Func<Type, bool> Filter { get; }
        
        public TypeFilterAttribute(Type filterType)
        {
            Filter = type => !type.IsAbstract &&
                             !type.IsInterface &&
                             !type.IsGenericType &&
                             type.InheritsOrImplements(filterType);
        }
    }
}