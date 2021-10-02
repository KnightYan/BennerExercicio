using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace Benner.Microondas.Comuns.Interfaces
{
    [TypeDescriptionProvider(typeof(CommandMapDescriptionProvider))]
    public class CommandMap
    {
        /// <summary>
        /// adicionando um novo comando
        /// </summary>
        /// <param name="nomeComando">Nome do comando (que será utilizado no wpf)</param>
        /// <param name="metodo">Methodo a ser executado</param>
        public ICommand AdicionarComando(string nomeComando, Action<object> metodo)
        {
            Comandos[nomeComando] = new DelegateCommand(metodo);
            return Comandos[nomeComando];
        }

        /// <summary>
        /// adicionando um novo comando,  (O comando deverá conter o mesmo nome informado no wpf)
        /// </summary>
        /// <param name="metodo">Metodo a ser executado (utilize o mesmo nome do método no wpf)</param>
        public ICommand AdicionarComando(Action metodo)
        {
            var nomeComando = metodo.Method.Name;
            Comandos[nomeComando] = new DelegateCommand(x => { metodo.Invoke(); });
            return Comandos[nomeComando];
        }

        /// <summary>
        /// adicionando um novo comando
        /// </summary>
        /// <param name="nomeComando">Nome do comando (que será utilizado no wpf)</param>
        /// <param name="metodo">Methodo a ser executado</param>
        /// <param name="metodoSeNaoExecutar">Methodo que será executado se nao executar</param>
        public void AdicionarComando(string nomeComando, Action<object> metodo, Predicate<object> metodoSeNaoExecutar)
        {
            Comandos[nomeComando] = new DelegateCommand(metodo, metodoSeNaoExecutar);
        }

        /// <summary>
        /// Remover um methodo
        /// </summary>
        /// <param name="nomeComando">Nome do comando</param>
        public void RemoverComando(string nomeComando)
        {
            Comandos.Remove(nomeComando);
        }

        public ICommand GetCommand(string nomeComando)
        {
            ICommand value;
            Comandos.TryGetValue(nomeComando, out value);
            return value;
        }

        /// <summary>
        /// Lista de comandos
        /// </summary>
        protected Dictionary<string, ICommand> Comandos
        {
            get
            {
                if (null == comandos)
                    comandos = new Dictionary<string, ICommand>();

                return comandos;
            }
        }

        /// <summary>
        /// Lista interna
        /// </summary>
        private Dictionary<string, ICommand> comandos;

        /// <summary>
        /// Implementacao do delegate de Comandos (ICommand)
        /// </summary>
        private class DelegateCommand : ICommand
        {
            public DelegateCommand(Action<object> executarMetodo) : this(executarMetodo, null)
            {
            }

            public DelegateCommand(Action<object> executarMetodo, Predicate<object> metodoSeNaoExecutar)
            {
                if (null == executarMetodo)
                    throw new ArgumentNullException("executeMethod");

                this._executeMethod = executarMetodo;
                this._canExecuteMethod = metodoSeNaoExecutar;
            }

            public bool CanExecute(object parameter)
            {
                return (null == _canExecuteMethod) ? true : _canExecuteMethod(parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                _executeMethod(parameter);
            }

            private readonly Predicate<object> _canExecuteMethod;
            private readonly Action<object> _executeMethod;
        }

        private class CommandMapDescriptionProvider : TypeDescriptionProvider
        {
            public CommandMapDescriptionProvider()
                : this(TypeDescriptor.GetProvider(typeof(CommandMap)))
            {
            }

            public CommandMapDescriptionProvider(TypeDescriptionProvider parent)
                : base(parent)
            {
            }

            public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
            {
                return new CommandMapDescriptor(base.GetTypeDescriptor(objectType, instance), instance as CommandMap);
            }
        }

        private class CommandMapDescriptor : CustomTypeDescriptor
        {
            public CommandMapDescriptor(ICustomTypeDescriptor descriptor, CommandMap map)
                : base(descriptor)
            {
                _map = map;
            }

            public override PropertyDescriptorCollection GetProperties()
            {
                PropertyDescriptor[] props = new PropertyDescriptor[_map.Comandos.Count];

                int pos = 0;

                foreach (KeyValuePair<string, ICommand> command in _map.Comandos)
                    props[pos++] = new CommandPropertyDescriptor(command);

                return new PropertyDescriptorCollection(props);
            }

            private readonly CommandMap _map;
        }

        private class CommandPropertyDescriptor : PropertyDescriptor
        {
            public CommandPropertyDescriptor(KeyValuePair<string, ICommand> command)
                : base(command.Key, null)
            {
                comandos = command.Value;
            }

            public override bool IsReadOnly
            {
                get { return true; }
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get { throw new NotImplementedException(); }
            }

            public override object GetValue(object component)
            {
                CommandMap map = component as CommandMap;

                if (null == map)
                    throw new ArgumentException("componente não esta instanciado n mapa de metodos", "componente");

                return map.Comandos[this.Name];
            }

            public override Type PropertyType
            {
                get { return typeof(ICommand); }
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }

            private readonly ICommand comandos;
        }
    }
}
