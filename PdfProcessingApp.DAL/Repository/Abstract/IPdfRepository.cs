using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.DAL.Repository.Abstract
{
    public interface IPdfRepository
    {
        void Create(PdfDocument document);
        PdfDocument GetById(int id);
        List<PdfDocument> GetAll();
        void Update(PdfDocument document);
        void Delete(int id);
        void LogCreation(PdfCreateLog log);
    }
}
