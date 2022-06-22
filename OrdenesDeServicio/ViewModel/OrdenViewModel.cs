using OrdenesDeServicio.DataAccess;
using OrdenesDeServicio.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrdenesDeServicio.ViewModel
{
    public class OrdenViewModel
    {
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private OrdenRepository _repository;
        private ordenServicio  _ordenEntity = null;
        public ordenRecord OrdenRecord { get; set; }
        public dbOrdenesEntities ordenEntities { get; set; }

        //Estados
        private ObservableCollection<Estados> _estados;
        public ObservableCollection<Estados> Estados
        {
            get { return _estados; }
            set { _estados = value; }
        }
        private Estados _estatus;
        public Estados Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }
        //

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteData((int)param), null);

                return _deleteCommand;
            }
        }
        public OrdenViewModel()
        {
            _ordenEntity = new ordenServicio();
            _repository = new OrdenRepository();
            OrdenRecord = new ordenRecord();
            GetAll();
            //Estados
            Estados = new ObservableCollection<Estados>()
            { new Estados(){ ID=1, Estado="Abierto"}
            ,new Estados(){ID=0,Estado="Cerrado"}};
        }
        public void ResetData()
        {
            OrdenRecord.id = 0;
            OrdenRecord.nombre = string.Empty;
            OrdenRecord.fecha = DateTime.Today;
            OrdenRecord.descripcion = string.Empty;
            OrdenRecord.estatus = -1;
        }
        public void DeleteData(int id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Student", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.RemoveOrden(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }
        public void SaveData()
        {
            if (OrdenRecord != null)
            {
                _ordenEntity.nombre = OrdenRecord.nombre;
                _ordenEntity.fecha = DateTime.Parse(OrdenRecord.fecha.ToString("yyyy-MM-dd"));
                _ordenEntity.descripcion = OrdenRecord.descripcion;
                _ordenEntity.estatus = OrdenRecord.estatus;

                try
                {
                    if (OrdenRecord.id <= 0)
                    {
                        _repository.AddOrden(_ordenEntity);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _ordenEntity.id = OrdenRecord.id;
                        _repository.UpdateOrden(_ordenEntity);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }
        public void EditData(int id)
        {
            var model = _repository.Get(id);
            OrdenRecord.id = model.id;
            OrdenRecord.nombre = model.nombre;
            OrdenRecord.fecha = DateTime.Parse(model.fecha.ToString("yyyy-MM-dd"));
            OrdenRecord.descripcion = model.descripcion;            
            OrdenRecord.estatus = model.estatus;
        }
        public void GetAll()
        {
            OrdenRecord.ordenRecords = new ObservableCollection<ordenRecord>();
            _repository.GetAll().ForEach(data => OrdenRecord.ordenRecords.Add(new ordenRecord()
            {
                id = data.id,
                nombre = data.nombre,
                fecha = data.fecha,
                descripcion = data.descripcion,
                estatus = data.estatus,
                estado = (int)data.estatus == 0 ? "Cerrado" : "Abierto"
            }));
        }
    }
}
