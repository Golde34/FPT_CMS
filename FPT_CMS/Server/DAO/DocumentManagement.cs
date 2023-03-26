using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Entity;

namespace Server.DAO
{
	public class DocumentManagement
	{
		private static DocumentManagement instance;
		private static readonly object instancelock = new object();

		public DocumentManagement()
		{
		}

		public static DocumentManagement Instance
		{
			get
			{
				lock (instancelock)
				{
					if (instance == null) instance = new DocumentManagement();
				}

				return instance;
			}
		}

		public Document AddDocument(Document Document)
		{
			try
			{
				Document _Document = GetDocumentById(Document.DocumentId);
				if (_Document == null)
				{
					var context = new AppDBContext();
					context.Documents.Add(_Document);
					context.SaveChanges();
					return _Document;
				}
				else
				{
					throw new Exception("The Document's username has already been taken.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Document UpdateDocument(Document Document)
		{
			try
			{
				Document _Document = GetDocumentById(Document.DocumentId);
				if (_Document != null)
				{
					var context = new AppDBContext();
					context.Entry<Document>(Document).State = EntityState.Modified;
					context.SaveChanges();
					return Document;
				}
				else
				{
					throw new Exception("The Document is not exist.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public Document DeleteDocument(Document Document)
		{
			try
			{
				Document _Document = GetDocumentById(Document.DocumentId);
				if (_Document != null)
				{
					var context = new AppDBContext();
					context.Documents.Remove(_Document);
					context.SaveChanges();
					return _Document;
				}
				else
				{
					throw new Exception("The Document is not exist.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<Document> GetDocuments()
		{
			List<Document> Documents;
			try
			{
				var context = new AppDBContext();
				Documents = context.Documents.ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return Documents;
		}

		public Document GetDocumentById(int? id)
		{
			Document Document;
			try
			{
				var _dbContext = new AppDBContext();
				Document = _dbContext.Documents
					.FirstOrDefault(a => a.DocumentId == id);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return Document;
		}

		public List<Document> GetDocumentsByCourseId(string? courseId)
		{
			List<Document> Documents;
			try
			{
				var _dbContext = new AppDBContext();
				Documents = _dbContext.Documents
					.Include(a => a.Account)
					.Where(c => c.CourseId == courseId).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return Documents;
		}

		public Document GetDocbyDate(DateTime? now, string accountId)
		{
			Document doc;
			try
			{
				var _dbContext = new AppDBContext();
				doc = _dbContext.Documents.Include(a => a.Account)
					.Where(a => a.AccountId == accountId && a.DocumentCreate == now).FirstOrDefault();
			}
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
			return doc;
        }

    }
}