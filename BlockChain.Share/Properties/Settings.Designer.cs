﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlockChain.Share.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=desktop-one\\MSSQLSERVER2019;Initial Catalog=WalletDb;Integrated Secur" +
            "ity=True")]
        public string UserDbConnectionString {
            get {
                return ((string)(this["UserDbConnectionString"]));
            }
            set {
                this["UserDbConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://goerli.etherscan.io/")]
        public string EtherSacn {
            get {
                return ((string)(this["EtherSacn"]));
            }
            set {
                this["EtherSacn"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsErc20ApproveMax {
            get {
                return ((bool)(this["IsErc20ApproveMax"]));
            }
            set {
                this["IsErc20ApproveMax"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int ChainId {
            get {
                return ((int)(this["ChainId"]));
            }
            set {
                this["ChainId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseLegacyAsDefault {
            get {
                return ((bool)(this["UseLegacyAsDefault"]));
            }
            set {
                this["UseLegacyAsDefault"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsTranslateOnLine {
            get {
                return ((bool)(this["IsTranslateOnLine"]));
            }
            set {
                this["IsTranslateOnLine"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WindowPosition {
            get {
                return ((string)(this["WindowPosition"]));
            }
            set {
                this["WindowPosition"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0ae3530fc6474a038493668f70c19beb")]
        public string MsTranslationKey {
            get {
                return ((string)(this["MsTranslationKey"]));
            }
            set {
                this["MsTranslationKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("eastasia")]
        public string MsTranslationLocation {
            get {
                return ((string)(this["MsTranslationLocation"]));
            }
            set {
                this["MsTranslationLocation"] = value;
            }
        }
    }
}
