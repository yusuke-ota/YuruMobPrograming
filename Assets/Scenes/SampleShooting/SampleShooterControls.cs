// GENERATED AUTOMATICALLY FROM 'Assets/Scenes/SampleShooting/SampleShooterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace SampleShooting
{
    public class @SampleShooterControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @SampleShooterControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""SampleShooterControls"",
    ""maps"": [
        {
            ""name"": ""Shooting"",
            ""id"": ""9f8c92aa-011f-401c-bc98-e0da51df204b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89deb7eb-81c8-4f3e-a478-558bc9e2c3d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""bcec19fa-534f-4643-ac96-733d92c0eccf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KeyBoardArrow"",
                    ""id"": ""ae577490-367b-44a5-9d4c-73237f23d993"",
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
                    ""id"": ""3107019b-51b7-47f6-aa44-23d0bfcc40e4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a2858bad-f15b-4efe-bac6-b59f11f06862"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""649bdf13-13f9-44b0-b452-a8e94d6c3f89"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""848cd368-7abb-45d1-b566-7217af161a41"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""KeyBoardWASD"",
                    ""id"": ""0f8ec332-5cb2-4071-975a-5f4bd6a6753f"",
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
                    ""id"": ""4f22a7e2-aa6a-4b2c-9cbe-38f0467a143d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8981cc27-8331-4989-baa2-9a22ad32c244"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ed9a8e3e-0247-4507-8579-59417320b4b7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46c795f0-a67d-43e4-a9c7-dd3cbb132ad6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6403c3d4-d40e-4b7c-bcb8-cf7889cb0357"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBoxController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf9fd866-c1a1-4e0b-ac72-d446f3f4899b"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBoxController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2422183f-4385-4e4f-824f-85fc96ca3748"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBoxController"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb3ba4d4-bb31-4c9b-89b2-fbf2ed1b7053"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46f2fbdd-5501-429d-83e7-25703984d196"",
                    ""path"": ""<Keyboard>/semicolon"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XBoxController"",
            ""bindingGroup"": ""XBoxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Shooting
            m_Shooting = asset.FindActionMap("Shooting", throwIfNotFound: true);
            m_Shooting_Move = m_Shooting.FindAction("Move", throwIfNotFound: true);
            m_Shooting_Shoot = m_Shooting.FindAction("Shoot", throwIfNotFound: true);
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

        // Shooting
        private readonly InputActionMap m_Shooting;
        private IShootingActions m_ShootingActionsCallbackInterface;
        private readonly InputAction m_Shooting_Move;
        private readonly InputAction m_Shooting_Shoot;
        public struct ShootingActions
        {
            private @SampleShooterControls m_Wrapper;
            public ShootingActions(@SampleShooterControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Shooting_Move;
            public InputAction @Shoot => m_Wrapper.m_Shooting_Shoot;
            public InputActionMap Get() { return m_Wrapper.m_Shooting; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ShootingActions set) { return set.Get(); }
            public void SetCallbacks(IShootingActions instance)
            {
                if (m_Wrapper.m_ShootingActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnMove;
                    @Shoot.started -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_ShootingActionsCallbackInterface.OnShoot;
                }
                m_Wrapper.m_ShootingActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                }
            }
        }
        public ShootingActions @Shooting => new ShootingActions(this);
        private int m_XBoxControllerSchemeIndex = -1;
        public InputControlScheme XBoxControllerScheme
        {
            get
            {
                if (m_XBoxControllerSchemeIndex == -1) m_XBoxControllerSchemeIndex = asset.FindControlSchemeIndex("XBoxController");
                return asset.controlSchemes[m_XBoxControllerSchemeIndex];
            }
        }
        private int m_KeyBoardSchemeIndex = -1;
        public InputControlScheme KeyBoardScheme
        {
            get
            {
                if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
                return asset.controlSchemes[m_KeyBoardSchemeIndex];
            }
        }
        public interface IShootingActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
        }
    }
}
