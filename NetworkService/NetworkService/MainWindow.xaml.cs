using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int id;
        private int value;
        private bool file = false;
        private Uri path = new Uri("Log.txt", UriKind.Relative);
        public MainWindow()
        {
            InitializeComponent();
            createListener(); //konekcija na server
        }
        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(GeneratoriViewModel.Generators.Count().ToString());
                            stream.Write(data, 0, data.Length);
                            file = false;
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"
                            string[] podeli = incomming.Split('_', ':');
                            int idx = Int32.Parse(podeli[1]); //cuvamo index
                            if (GeneratoriViewModel.Generators.Count > idx)
                                id = GeneratoriViewModel.Generators[idx].Id;
                            else
                                id = -1;
                            value = Int32.Parse(podeli[2]);

                            if (id != -1)
                            {
                                GeneratoriViewModel.Generators[idx].Value = value;
                                FileWrite();
                            }

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();

        }
        private void FileWrite()
        {
            if (!file)
            {
                StreamWriter sw;
                using (sw = new StreamWriter(path.ToString()))
                    sw.WriteLine("Datum, Vreme: " + DateTime.Now.ToString() + " Entitet:\t" + id + " Vrednost:\t" + value);
            }
            else
            {
                StreamWriter sw;
                using (sw = new StreamWriter(path.ToString(), true))
                    sw.WriteLine("Datum, Vreme: " + DateTime.Now.ToString() + " Entitet:\t" + id + " Vrednost:\t" + value);
            }
            file = true;
        }
    }
}

