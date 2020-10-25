using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnyRPG {

    /// <summary>
    /// provide access to a UUID component
    /// </summary>
    public interface IPersistentObjectOwner {

        UUID UUID { get; }
        Transform transform { get; }
        PersistentObjectComponent PersistentObjectComponent { get; }
    }

}