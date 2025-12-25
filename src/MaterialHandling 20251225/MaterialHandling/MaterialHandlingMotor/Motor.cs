using System.ComponentModel;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingUI.UIFrame.Control;
using System.Linq;

namespace MaterialHandling.MaterialHandlingMotor
{
    // 定义电机状态枚举
    public enum MotorState
    {
        Forward = 0,  // 正向运行
        Backward = 1, // 反向运行
        Stop = 2,     // 休息中
        Fault = 3     // 故障
    }

    // 基类 Motor
    public class Motor : INotifyPropertyChanged
    {
        private string _name;
        private int _id;
        private string _type;
        private float _speed;
        private MotorState _currentState;

        public bool Forward;
        public bool Backward;
        public bool Run;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged(nameof(Speed));
                }
            }
        }

        public MotorState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    OnPropertyChanged(nameof(CurrentState));
                }
            }
        }

        public Motor(string name, int id, string type, float speed = 10f)
        {
            Name = name;
            Id = id;
            Type = type;
            Speed = speed;
            CurrentState = MotorState.Stop;
        }

        public virtual void Stop()
        {
            CurrentState = MotorState.Stop;
        }

        public virtual void SetFault()
        {
            CurrentState = MotorState.Fault;
        }

        // 实现 INotifyPropertyChanged 接口
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void BindMotorToControl(MotorPanel mcf)
        {
            // 根据电机对象的 Name 属性查找控件
            var control = mcf.Controls.Find(this.Name, true).FirstOrDefault();
            if (control == null)
            {
                MessageBox.Show($"控件 {this.Name} 未找到，请确保控件名称与电机对象的 Name 属性一致。");
                return;
            }

            // 绑定 Speed 属性
            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Motor.Speed))
                {
                    var speedStateProperty = control.GetType().GetProperty("Speed_State");
                    if (speedStateProperty != null)
                    {
                        speedStateProperty.SetValue(control, this.Speed.ToString());
                    }
                }
            };

            // 绑定 CurrentState 属性
            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Motor.CurrentState))
                {
                    string stateSymbol;
                    switch (this.CurrentState)
                    {
                        case MotorState.Forward:
                            stateSymbol = "→";
                            break;
                        case MotorState.Backward:
                            stateSymbol = "←";
                            break;
                        case MotorState.Fault:
                            stateSymbol = "";
                            break;
                        case MotorState.Stop:
                        default:
                            stateSymbol = " ";
                            break;
                    }

                    var currentStateProperty = control.GetType().GetProperty("CurrentState");
                    if (currentStateProperty != null)
                    {
                        currentStateProperty.SetValue(control, stateSymbol);
                    }
                }
            };

            // 绑定 Id 属性到 MotorCode
            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Motor.Id))
                {
                    var motorCodeProperty = control.GetType().GetProperty("MotorCode");
                    if (motorCodeProperty != null)
                    {
                        motorCodeProperty.SetValue(control, string.Format("M{0}", this.Id.ToString()));
                    }
                }
            };

            // 设置 BtnBackwardText 和 BtnForwardText
            string backwardText;
            string forwardText;

            switch (this.Type)
            {
                case "Belt":
                    backwardText = "反转";
                    forwardText = "正转";
                    break;
                case "Lifter":
                    backwardText = "下降";
                    forwardText = "上升";
                    break;
                case "Stretch":
                    backwardText = "收回";
                    forwardText = "伸出";
                    break;
                default:
                    backwardText = "Backward";
                    forwardText = "Forward";
                    break;
            }

            var btnBackwardTextProperty = control.GetType().GetProperty("BtnBackwardText");
            if (btnBackwardTextProperty != null)
            {
                btnBackwardTextProperty.SetValue(control, backwardText);
            }

            var btnForwardTextProperty = control.GetType().GetProperty("BtnForwardText");
            if (btnForwardTextProperty != null)
            {
                btnForwardTextProperty.SetValue(control, forwardText);
            }

            // 初始化控件属性
            var speedStatePropertyInit = control.GetType().GetProperty("Speed_State");
            if (speedStatePropertyInit != null)
            {
                speedStatePropertyInit.SetValue(control, this.Speed.ToString());
            }

            var currentStatePropertyInit = control.GetType().GetProperty("CurrentState");
            if (currentStatePropertyInit != null)
            {
                string stateSymbolInit;
                switch (this.CurrentState)
                {
                    case MotorState.Forward:
                        stateSymbolInit = "→";
                        break;
                    case MotorState.Backward:
                        stateSymbolInit = "←";
                        break;
                    case MotorState.Fault:
                        stateSymbolInit = "";
                        break;
                    case MotorState.Stop:
                    default:
                        stateSymbolInit = " ";
                        break;
                }
                currentStatePropertyInit.SetValue(control, stateSymbolInit);
            }

            // 初始化 MotorCode 属性
            var motorCodePropertyInit = control.GetType().GetProperty("MotorCode");
            if (motorCodePropertyInit != null)
            {
                motorCodePropertyInit.SetValue(control, string.Format("M{0}", this.Id.ToString()));
            }
        }
    }

    // 子类 LifterMotor（升降电机）
    public class LifterMotor : Motor
    {
        public float AO { get; set; } // 当前高度

        public LifterMotor(string name, int id, float speed = 10, float ao = 0)
            : base(name, id, "Lifter", speed)
        {
            AO = ao;
        }

    }

    // 子类 StretchMotor（伸缩电机）
    public class StretchMotor : Motor
    {
        public StretchMotor(string name, int id, float speed = 10)
            : base(name, id, "Stretch", speed)
        {
        }

    }

    // 子类 BeltMotor（皮带电机/旋转电机）
    public class BeltMotor : Motor
    {
        public BeltMotor(string name, int id, float speed = 10)
            : base(name, id, "Belt", speed)
        {
        }
    }

    // 子类 ClampMotor（夹爪电机，带升降功能）
    public class ClampMotor : Motor
    {
        public bool IsClamped { get; private set; } // 是否夹紧
        public float GripForce { get; set; } // 夹紧力
        public float AO { get; set; } // 当前高度

        public ClampMotor(string name, int id, float speed = 10, float gripForce = 0, float ao = 0)
            : base(name, id, "Clamp", speed)
        {
            IsClamped = false;
            GripForce = gripForce;
            AO = ao;
        }

        public void Clamp()
        {
            IsClamped = true;
            CurrentState = MotorState.Forward; // 夹紧可视为正向运行
        }

        public void Release()
        {
            IsClamped = false;
            CurrentState = MotorState.Stop;
        }

      
       
    }
}