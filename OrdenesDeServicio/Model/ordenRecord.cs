using OrdenesDeServicio.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenesDeServicio.Model
{
    public class ordenRecord : ViewModelBase
    {
        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }

        private string _nombre;
        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                OnPropertyChanged("nombre");
            }
        }

        private DateTime _fecha;
        public DateTime fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
                OnPropertyChanged("fecha");
            }
        }

        private string _descripcion;
        public string descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                OnPropertyChanged("descripcion");
            }
        }

        private int _estatus;
        public int estatus
        {
            get
            {
                return _estatus;
            }
            set
            {
                _estatus = value;
                OnPropertyChanged("estatus");
            }
        }

        private string _estado;
        public string estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
                OnPropertyChanged("estado");
            }
        }

        private ObservableCollection<ordenRecord> _ordenRecords;
        public ObservableCollection<ordenRecord> ordenRecords
        {
            get
            {
                return _ordenRecords;
            }
            set
            {
                _ordenRecords = value;
                OnPropertyChanged("ordenRecords");
            }
        }

        private void OrdenModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ordenRecords");
        }
    }
}
