using StreamingManagement.Models.dto.PSS.ServiceType;
using StreamingManagement.Utils.Consultas.PSS;
using System.Data;

namespace StreamingManagement.Utils.PSS
{
    public class ManageOperation
    {
        private PSSConsulting _consulting;

        public ManageOperation() { 
            _consulting = new PSSConsulting();
        }

        public ResponseServiceType getService()
        {
            List<ServiceTypeDTO> serviceTypes = new List<ServiceTypeDTO>();

            DataTable arregloCursor = _consulting.getAllSucriptions();

            if (arregloCursor != null)
            {
                foreach (DataRow row in arregloCursor.Rows) {
                    serviceTypes.Add(
                        new ServiceTypeDTO { 
                                service_type_id = Convert.ToInt32(row["SERVICE_TYPE_ID"]),
                                service_name = row["NOMBRE"].ToString()
                        }
                        );
                }

            }
            


            return new ResponseServiceType { 
                description = "Devuelve todos los servicios disponibles",
                servicesList = serviceTypes
            };
        }
    }
}
