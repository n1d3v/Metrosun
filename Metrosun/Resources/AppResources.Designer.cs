﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metrosun.Resources
{
    using System;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Metrosun.Resources.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to LeftToRight.
        /// </summary>
        public static string ResourceFlowDirection
        {
            get
            {
                return ResourceManager.GetString("ResourceFlowDirection", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to us-EN.
        /// </summary>
        public static string ResourceLanguage
        {
            get
            {
                return ResourceManager.GetString("ResourceLanguage", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MY APPLICATION.
        /// </summary>
        public static string ApplicationTitle
        {
            get
            {
                return ResourceManager.GetString("ApplicationTitle", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to button.
        /// </summary>
        public static string AppBarButtonText
        {
            get
            {
                return ResourceManager.GetString("AppBarButtonText", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to menu item.
        /// </summary>
        public static string AppBarMenuItemText
        {
            get
            {
                return ResourceManager.GetString("AppBarMenuItemText", resourceCulture);
            }
        }
    }
}
