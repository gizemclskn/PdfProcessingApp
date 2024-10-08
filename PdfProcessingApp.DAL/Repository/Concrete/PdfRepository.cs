using PdfProcessingApp.DAL.Repository.Abstract;
using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.DAL.Repository.Concrete
{
    public class PdfRepository : IPdfRepository
    {
        private readonly AppDbContext _context;
        private readonly string _outputDirectory; 
        private readonly List<DocumentSection> _documentSections; 
       
        public PdfRepository(AppDbContext context, string outputDirectory, List<DocumentSection> documentSections)
        {
            _context = context;
            _outputDirectory = outputDirectory; 
            _documentSections = documentSections; 
        }

        public void Create(PdfDocument document)
        {
            _context.PdfDocuments.Add(document);
            _context.SaveChanges();
        }

        public PdfDocument GetById(int id)
        {
            return _context.PdfDocuments.Find(id);
        }

        public List<PdfDocument> GetAll()
        {
            return _context.PdfDocuments.ToList();
        }

        public void Update(PdfDocument document)
        {
            _context.PdfDocuments.Update(document);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var document = _context.PdfDocuments.Find(id);
            if (document != null)
            {
                _context.PdfDocuments.Remove(document);
                _context.SaveChanges();
            }
        }

        public void LogCreation(PdfCreateLog log)
        {
            _context.PdfCreateLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
