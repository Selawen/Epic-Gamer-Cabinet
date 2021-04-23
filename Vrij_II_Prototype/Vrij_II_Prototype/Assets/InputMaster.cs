// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""27b86665-c4f0-4cb3-b174-b703a3bb41d9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3aec13d5-a2f3-47b2-ade6-51abf0944bba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Yellow"",
                    ""type"": ""Button"",
                    ""id"": ""a73dce2d-5808-4ff8-af26-a788fc8ca9db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Red"",
                    ""type"": ""Button"",
                    ""id"": ""d38c5831-076d-4ccb-aff5-fa79e96bb2e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Green"",
                    ""type"": ""Button"",
                    ""id"": ""e7a46925-339b-438b-8f47-2ad372f065c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Blue"",
                    ""type"": ""Button"",
                    ""id"": ""0d19d704-93e2-4d69-ae00-e60ed627ad93"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d3e457a4-47b5-4ff6-9d7e-4569212879c4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8b00fd28-30d7-40d0-8710-9a73e39748f7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""67592751-db9d-4668-9af2-c97b1884df64"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""beba80cd-b0c2-4ae0-8be8-48dfcde4ea38"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1b4147ac-3f8e-4ad1-b263-880bceb142a3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ee2710a4-866c-4ba9-ad1b-cad22e650017"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Yellow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""051a7ead-bd23-4508-8cc1-233780b33d08"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Red"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b75aa956-f766-434a-ba11-5b956e1a4e04"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Green"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a65c357b-5421-4fb8-bc9b-ec8be368de56"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox one controller"",
                    ""action"": ""Blue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XBox one controller"",
            ""bindingGroup"": ""XBox one controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XboxOneGampadiOS>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Yellow = m_Player.FindAction("Yellow", throwIfNotFound: true);
        m_Player_Red = m_Player.FindAction("Red", throwIfNotFound: true);
        m_Player_Green = m_Player.FindAction("Green", throwIfNotFound: true);
        m_Player_Blue = m_Player.FindAction("Blue", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Yellow;
    private readonly InputAction m_Player_Red;
    private readonly InputAction m_Player_Green;
    private readonly InputAction m_Player_Blue;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Yellow => m_Wrapper.m_Player_Yellow;
        public InputAction @Red => m_Wrapper.m_Player_Red;
        public InputAction @Green => m_Wrapper.m_Player_Green;
        public InputAction @Blue => m_Wrapper.m_Player_Blue;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Yellow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYellow;
                @Yellow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYellow;
                @Yellow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYellow;
                @Red.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRed;
                @Red.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRed;
                @Red.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRed;
                @Green.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGreen;
                @Green.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGreen;
                @Green.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGreen;
                @Blue.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlue;
                @Blue.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlue;
                @Blue.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlue;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Yellow.started += instance.OnYellow;
                @Yellow.performed += instance.OnYellow;
                @Yellow.canceled += instance.OnYellow;
                @Red.started += instance.OnRed;
                @Red.performed += instance.OnRed;
                @Red.canceled += instance.OnRed;
                @Green.started += instance.OnGreen;
                @Green.performed += instance.OnGreen;
                @Green.canceled += instance.OnGreen;
                @Blue.started += instance.OnBlue;
                @Blue.performed += instance.OnBlue;
                @Blue.canceled += instance.OnBlue;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_XBoxonecontrollerSchemeIndex = -1;
    public InputControlScheme XBoxonecontrollerScheme
    {
        get
        {
            if (m_XBoxonecontrollerSchemeIndex == -1) m_XBoxonecontrollerSchemeIndex = asset.FindControlSchemeIndex("XBox one controller");
            return asset.controlSchemes[m_XBoxonecontrollerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnYellow(InputAction.CallbackContext context);
        void OnRed(InputAction.CallbackContext context);
        void OnGreen(InputAction.CallbackContext context);
        void OnBlue(InputAction.CallbackContext context);
    }
}
