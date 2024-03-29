// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/com/Gale/Core/Input/Input Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace com.Gale.Input
{
    public class @InputController : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputController()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Controller"",
    ""maps"": [
        {
            ""name"": ""Player 1"",
            ""id"": ""61c4e767-79a5-4cc2-8d1f-7419b30081e3"",
            ""actions"": [
                {
                    ""name"": ""Vertical Movement"",
                    ""type"": ""Value"",
                    ""id"": ""85ccef90-4395-4f77-aaf9-38f792f23454"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6fd27a32-0a19-46d2-bfd7-9184f6bdb934"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Controller"",
                    ""id"": ""8c25f98d-029d-4aa9-b9fb-731d276817a8"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""26dcbe73-d183-4c2a-b844-8f0dedb1b30d"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""40ed2704-9f28-4543-8ce5-48fb79a2c4d9"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a895e78d-0170-49b0-802b-1cf0b084c1fc"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""caed96af-1e96-4868-8d28-4048074b2497"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""93cb546a-d125-4b72-b123-0a2cde585acd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""a7fff128-172e-4229-b783-2387f76011a6"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6fdfdae8-4071-4f20-af57-58cf2b0768f5"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0098f211-807b-40ea-9fb1-73308d550715"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e159090e-b3af-4c9b-9314-019aa8778fe2"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8d8f086-5d66-446c-ad73-b5bea4c11158"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player 2"",
            ""id"": ""14326160-c6df-4d91-ace9-769c07e6b716"",
            ""actions"": [
                {
                    ""name"": ""Vertical Movement"",
                    ""type"": ""Value"",
                    ""id"": ""88231864-c909-4ac8-b230-1c120ca1be41"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Controller"",
                    ""id"": ""d2e609b9-ecb6-4092-b169-3eb85d3c3220"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""814dffb5-2035-43a2-9fe3-0c03e8deceed"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7b333da6-3d54-462a-ad1a-b18ece0e4ac5"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""5a4c6603-a90f-4c29-8cd0-ee53de21a89b"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4172d802-4d18-4b9c-a7c9-78bdbac9227c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5d0cd61f-a48d-46eb-b60e-60568c40c0ee"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""40fe059d-5e53-43bd-93b2-08ed2ae04b5d"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a13284e1-d504-4b1e-a2dd-4d3a62493429"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""45ead76a-6202-412b-9dc0-64ac4e95507c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player 1
            m_Player1 = asset.FindActionMap("Player 1", throwIfNotFound: true);
            m_Player1_VerticalMovement = m_Player1.FindAction("Vertical Movement", throwIfNotFound: true);
            m_Player1_Pause = m_Player1.FindAction("Pause", throwIfNotFound: true);
            // Player 2
            m_Player2 = asset.FindActionMap("Player 2", throwIfNotFound: true);
            m_Player2_VerticalMovement = m_Player2.FindAction("Vertical Movement", throwIfNotFound: true);
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

        // Player 1
        private readonly InputActionMap m_Player1;
        private IPlayer1Actions m_Player1ActionsCallbackInterface;
        private readonly InputAction m_Player1_VerticalMovement;
        private readonly InputAction m_Player1_Pause;
        public struct Player1Actions
        {
            private @InputController m_Wrapper;
            public Player1Actions(@InputController wrapper) { m_Wrapper = wrapper; }
            public InputAction @VerticalMovement => m_Wrapper.m_Player1_VerticalMovement;
            public InputAction @Pause => m_Wrapper.m_Player1_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Player1; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
            public void SetCallbacks(IPlayer1Actions instance)
            {
                if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
                {
                    @VerticalMovement.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnVerticalMovement;
                    @VerticalMovement.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnVerticalMovement;
                    @VerticalMovement.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnVerticalMovement;
                    @Pause.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_Player1ActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @VerticalMovement.started += instance.OnVerticalMovement;
                    @VerticalMovement.performed += instance.OnVerticalMovement;
                    @VerticalMovement.canceled += instance.OnVerticalMovement;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public Player1Actions @Player1 => new Player1Actions(this);

        // Player 2
        private readonly InputActionMap m_Player2;
        private IPlayer2Actions m_Player2ActionsCallbackInterface;
        private readonly InputAction m_Player2_VerticalMovement;
        public struct Player2Actions
        {
            private @InputController m_Wrapper;
            public Player2Actions(@InputController wrapper) { m_Wrapper = wrapper; }
            public InputAction @VerticalMovement => m_Wrapper.m_Player2_VerticalMovement;
            public InputActionMap Get() { return m_Wrapper.m_Player2; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
            public void SetCallbacks(IPlayer2Actions instance)
            {
                if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
                {
                    @VerticalMovement.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVerticalMovement;
                    @VerticalMovement.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVerticalMovement;
                    @VerticalMovement.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVerticalMovement;
                }
                m_Wrapper.m_Player2ActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @VerticalMovement.started += instance.OnVerticalMovement;
                    @VerticalMovement.performed += instance.OnVerticalMovement;
                    @VerticalMovement.canceled += instance.OnVerticalMovement;
                }
            }
        }
        public Player2Actions @Player2 => new Player2Actions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IPlayer1Actions
        {
            void OnVerticalMovement(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
        public interface IPlayer2Actions
        {
            void OnVerticalMovement(InputAction.CallbackContext context);
        }
    }
}
