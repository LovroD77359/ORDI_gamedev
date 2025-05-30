//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/InputAssigments.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputAssigments: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputAssigments()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputAssigments"",
    ""maps"": [
        {
            ""name"": ""ActMap"",
            ""id"": ""a844052e-aca7-4021-b8d5-f32a78aa8d91"",
            ""actions"": [
                {
                    ""name"": ""JumpSpace"",
                    ""type"": ""Button"",
                    ""id"": ""a90a70b5-f2f4-4998-a7db-ee61fff89edd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveUp"",
                    ""type"": ""Button"",
                    ""id"": ""e680bfe4-4de1-432e-9db6-6b7edf94db70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveDown"",
                    ""type"": ""Button"",
                    ""id"": ""bd8e12ea-059c-400b-9319-0e4a12d9324d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""7f399940-8263-4fd3-bfe0-0baf008c20ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveRight"",
                    ""type"": ""Button"",
                    ""id"": ""f473c261-cbaf-481e-9f45-9cc927a10c1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveW"",
                    ""type"": ""Button"",
                    ""id"": ""82508fc4-3e7a-476e-a813-4d7994b8e0f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveS"",
                    ""type"": ""Button"",
                    ""id"": ""1e1bab6e-55c8-4ef4-bedf-ba1cf417459b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveA"",
                    ""type"": ""Button"",
                    ""id"": ""4a8bc6ba-4bd6-4546-8141-9a21fdd29595"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""moveD"",
                    ""type"": ""Button"",
                    ""id"": ""f7a6d9b9-97fa-4a1a-8ba8-7e5a441d7053"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveWASD"",
                    ""type"": ""Button"",
                    ""id"": ""ec6df362-f6e4-477f-af9f-e17a20f71a26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2f00275-5d78-4b9b-b314-b3ea92ed70cd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpSpace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ff9f15e-2d12-4662-8cd5-123fd5f8230b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6efa074-37cb-442e-95f0-f48b526a3f39"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eacbdfd-4e72-4502-95f9-daec042598dc"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf149936-d9f0-467b-8e1a-dbbe6bedfdd8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb43087f-3aa2-401f-a03c-9d4693454c24"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveW"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f361bf9-305b-4c62-a487-5a7131c0bd06"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a17ed5d4-878e-4e86-9746-149c5b0b2578"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ca825a2-ca93-49f2-aefe-e1d0014f2795"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1e9a406-3ee3-47fa-a59a-8cc775b55e9e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveWASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActMap
        m_ActMap = asset.FindActionMap("ActMap", throwIfNotFound: true);
        m_ActMap_JumpSpace = m_ActMap.FindAction("JumpSpace", throwIfNotFound: true);
        m_ActMap_moveUp = m_ActMap.FindAction("moveUp", throwIfNotFound: true);
        m_ActMap_moveDown = m_ActMap.FindAction("moveDown", throwIfNotFound: true);
        m_ActMap_moveLeft = m_ActMap.FindAction("moveLeft", throwIfNotFound: true);
        m_ActMap_moveRight = m_ActMap.FindAction("moveRight", throwIfNotFound: true);
        m_ActMap_moveW = m_ActMap.FindAction("moveW", throwIfNotFound: true);
        m_ActMap_moveS = m_ActMap.FindAction("moveS", throwIfNotFound: true);
        m_ActMap_moveA = m_ActMap.FindAction("moveA", throwIfNotFound: true);
        m_ActMap_moveD = m_ActMap.FindAction("moveD", throwIfNotFound: true);
        m_ActMap_MoveWASD = m_ActMap.FindAction("MoveWASD", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ActMap
    private readonly InputActionMap m_ActMap;
    private List<IActMapActions> m_ActMapActionsCallbackInterfaces = new List<IActMapActions>();
    private readonly InputAction m_ActMap_JumpSpace;
    private readonly InputAction m_ActMap_moveUp;
    private readonly InputAction m_ActMap_moveDown;
    private readonly InputAction m_ActMap_moveLeft;
    private readonly InputAction m_ActMap_moveRight;
    private readonly InputAction m_ActMap_moveW;
    private readonly InputAction m_ActMap_moveS;
    private readonly InputAction m_ActMap_moveA;
    private readonly InputAction m_ActMap_moveD;
    private readonly InputAction m_ActMap_MoveWASD;
    public struct ActMapActions
    {
        private @InputAssigments m_Wrapper;
        public ActMapActions(@InputAssigments wrapper) { m_Wrapper = wrapper; }
        public InputAction @JumpSpace => m_Wrapper.m_ActMap_JumpSpace;
        public InputAction @moveUp => m_Wrapper.m_ActMap_moveUp;
        public InputAction @moveDown => m_Wrapper.m_ActMap_moveDown;
        public InputAction @moveLeft => m_Wrapper.m_ActMap_moveLeft;
        public InputAction @moveRight => m_Wrapper.m_ActMap_moveRight;
        public InputAction @moveW => m_Wrapper.m_ActMap_moveW;
        public InputAction @moveS => m_Wrapper.m_ActMap_moveS;
        public InputAction @moveA => m_Wrapper.m_ActMap_moveA;
        public InputAction @moveD => m_Wrapper.m_ActMap_moveD;
        public InputAction @MoveWASD => m_Wrapper.m_ActMap_MoveWASD;
        public InputActionMap Get() { return m_Wrapper.m_ActMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActMapActions set) { return set.Get(); }
        public void AddCallbacks(IActMapActions instance)
        {
            if (instance == null || m_Wrapper.m_ActMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActMapActionsCallbackInterfaces.Add(instance);
            @JumpSpace.started += instance.OnJumpSpace;
            @JumpSpace.performed += instance.OnJumpSpace;
            @JumpSpace.canceled += instance.OnJumpSpace;
            @moveUp.started += instance.OnMoveUp;
            @moveUp.performed += instance.OnMoveUp;
            @moveUp.canceled += instance.OnMoveUp;
            @moveDown.started += instance.OnMoveDown;
            @moveDown.performed += instance.OnMoveDown;
            @moveDown.canceled += instance.OnMoveDown;
            @moveLeft.started += instance.OnMoveLeft;
            @moveLeft.performed += instance.OnMoveLeft;
            @moveLeft.canceled += instance.OnMoveLeft;
            @moveRight.started += instance.OnMoveRight;
            @moveRight.performed += instance.OnMoveRight;
            @moveRight.canceled += instance.OnMoveRight;
            @moveW.started += instance.OnMoveW;
            @moveW.performed += instance.OnMoveW;
            @moveW.canceled += instance.OnMoveW;
            @moveS.started += instance.OnMoveS;
            @moveS.performed += instance.OnMoveS;
            @moveS.canceled += instance.OnMoveS;
            @moveA.started += instance.OnMoveA;
            @moveA.performed += instance.OnMoveA;
            @moveA.canceled += instance.OnMoveA;
            @moveD.started += instance.OnMoveD;
            @moveD.performed += instance.OnMoveD;
            @moveD.canceled += instance.OnMoveD;
            @MoveWASD.started += instance.OnMoveWASD;
            @MoveWASD.performed += instance.OnMoveWASD;
            @MoveWASD.canceled += instance.OnMoveWASD;
        }

        private void UnregisterCallbacks(IActMapActions instance)
        {
            @JumpSpace.started -= instance.OnJumpSpace;
            @JumpSpace.performed -= instance.OnJumpSpace;
            @JumpSpace.canceled -= instance.OnJumpSpace;
            @moveUp.started -= instance.OnMoveUp;
            @moveUp.performed -= instance.OnMoveUp;
            @moveUp.canceled -= instance.OnMoveUp;
            @moveDown.started -= instance.OnMoveDown;
            @moveDown.performed -= instance.OnMoveDown;
            @moveDown.canceled -= instance.OnMoveDown;
            @moveLeft.started -= instance.OnMoveLeft;
            @moveLeft.performed -= instance.OnMoveLeft;
            @moveLeft.canceled -= instance.OnMoveLeft;
            @moveRight.started -= instance.OnMoveRight;
            @moveRight.performed -= instance.OnMoveRight;
            @moveRight.canceled -= instance.OnMoveRight;
            @moveW.started -= instance.OnMoveW;
            @moveW.performed -= instance.OnMoveW;
            @moveW.canceled -= instance.OnMoveW;
            @moveS.started -= instance.OnMoveS;
            @moveS.performed -= instance.OnMoveS;
            @moveS.canceled -= instance.OnMoveS;
            @moveA.started -= instance.OnMoveA;
            @moveA.performed -= instance.OnMoveA;
            @moveA.canceled -= instance.OnMoveA;
            @moveD.started -= instance.OnMoveD;
            @moveD.performed -= instance.OnMoveD;
            @moveD.canceled -= instance.OnMoveD;
            @MoveWASD.started -= instance.OnMoveWASD;
            @MoveWASD.performed -= instance.OnMoveWASD;
            @MoveWASD.canceled -= instance.OnMoveWASD;
        }

        public void RemoveCallbacks(IActMapActions instance)
        {
            if (m_Wrapper.m_ActMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActMapActions instance)
        {
            foreach (var item in m_Wrapper.m_ActMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActMapActions @ActMap => new ActMapActions(this);
    public interface IActMapActions
    {
        void OnJumpSpace(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveW(InputAction.CallbackContext context);
        void OnMoveS(InputAction.CallbackContext context);
        void OnMoveA(InputAction.CallbackContext context);
        void OnMoveD(InputAction.CallbackContext context);
        void OnMoveWASD(InputAction.CallbackContext context);
    }
}
