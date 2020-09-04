using UnityEngine;
///<summary>
///Attribute that require implementation of the provided interface.
///</summary>
public class RequireInterfaceAttribute : PropertyAttribute
{
    public System.Type RequiredType
    {
        get;
        private set;
    }

    ///<summary>
    ///Requiring implementation of the
    ///<see cref="T:RequireInterfaceAttribute"/> interface.
    ///</summary>
    ///<param name="type">Interface type.</param>
    public RequireInterfaceAttribute(System.Type type)
    {
        RequiredType = type;
    }
}