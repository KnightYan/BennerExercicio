using Benner.Microondas.Comuns.Interfaces;
using Benner.Microondas.Controle.Controles;
using Benner.Microondas.Controle.Controles.Validadadores;
using Benner.Microondas.Model.Dto;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;

namespace Benner.Microondas.Interfaces.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MicroondasVm
    {
        private readonly ProgramasControle controle;

        public CommandMap Comandos { get; set; }
        public List<ProgramaDto> ListaProgramas { get; set; }
        public ProgramaDto ProgramaSelecionado { get; set; }
        public string PainelSaida { get; set; }
        public int PainelTempoRestante { get; set; }
        public bool PodeEditarPrograma { get; set; }
        public bool PodeAquecer { get; set; }
        public bool Aquecendo { get; set; }
        public MicroondasVm()
        {
            controle = new ProgramasControle();
            ListaProgramas = controle.ObterProgramas();

            Comandos = new CommandMap();

            Comandos.AdicionarComando("IniciarPrograma", x => IniciarPrograma());
            Comandos.AdicionarComando("IniciarRapido", x => IniciarRapido());
            Comandos.AdicionarComando("SalvarPrograma", x => SalvarPrograma());
            Comandos.AdicionarComando("CriarPrograma", x => CriarPrograma());

            PodeEditarPrograma = true;
            PodeAquecer = true;
            ProgramaSelecionado = new ProgramaDto();
        }

        public void IniciarPrograma()
        {
            Thread tarefa = new Thread(AquecimentoPorPrograma);
            tarefa.Start();
        }

        public void IniciarRapido()
        {
            Thread tarefa = new Thread(AquecimentoRapido);
            tarefa.Start();
        }

        private void AquecimentoPorPrograma()
        {
            Bloquear();
            try
            {
                new ProgramaValidador().ValidarPrograma(ProgramaSelecionado);
                Aquecimento(ProgramaSelecionado);
            }
            catch(Exception exception)
            {
                PainelSaida = exception.Message;
            }
            Desbloquear();
        }

        private void AquecimentoRapido()
        {
            Bloquear();
            var programa = new ProgramaDto
            {
                CaractereAquecimento = ".",
                Tempo = 30,
                Potencia = 8
            };
            Aquecimento(programa);
            Desbloquear();
        }

        private void Bloquear()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                PodeAquecer = false;
                PodeEditarPrograma = false;
                Aquecendo = true;
            });
        }

        private void Desbloquear()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                PodeAquecer = true;
                PodeEditarPrograma = true;
                Aquecendo = false;
            });
        }

        private void Aquecimento(ProgramaDto programa)
        {
            PainelSaida = "";
            PainelTempoRestante = 0;

            var tempoRestante = programa.Tempo ?? 0;
            var potenciaPorSegundo = GerarPotenciaPorSegundo(programa.Potencia, programa.CaractereAquecimento);

            do
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    PainelTempoRestante = tempoRestante;
                    PainelSaida += potenciaPorSegundo;
                });

                tempoRestante--;
                Thread.Sleep(1000);

            } while (tempoRestante >=0);

            MessageBox.Show("Pronto!");
        }

        private string GerarPotenciaPorSegundo(int? potencia, string caractereAquecimento)
        {
            string resultado="";
            for (int i = 0; i < potencia; i++)
                resultado += caractereAquecimento;

            return resultado;
        }

        public void SalvarPrograma()
        {
            try
            {
                new ProgramaValidador().ValidarPrograma(ProgramaSelecionado);
                controle.SalvarPrograma(ProgramaSelecionado);

                ListaProgramas = controle.ObterProgramas();
            }
            catch (Exception exception)
            {
                PainelSaida = exception.Message;
            }
        }

        public void CriarPrograma()
        {
            ProgramaSelecionado = new ProgramaDto();
        }
    }
}
