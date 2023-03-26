using Microsoft.EntityFrameworkCore;
using Server.DTO;
using Server.Entity;

namespace Server.DAO
{
	public class DocumentFileManagement
	{
		private static DocumentFileManagement instance;
		private static readonly object instancelock = new object();

		public DocumentFileManagement()
		{
		}

		public static DocumentFileManagement Instance
		{
			get
			{
				lock (instancelock)
				{
					if (instance == null) instance = new DocumentFileManagement();
				}

				return instance;
			}
		}

		public DocumentFile AddDocumentFile(DocumentFile DocumentFile)
		{
			try
			{
				DocumentFile _DocumentFile = GetDocumentFileById(DocumentFile.Id);
				if (_DocumentFile == null)
				{
					var context = new AppDBContext();
					context.DocumentFiles.Add(DocumentFile);
					context.SaveChanges();
					return DocumentFile;
				}
				else
				{
					throw new Exception("The DocumentFile has already been taken.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public DocumentFile UpdateDocumentFile(DocumentFile DocumentFile)
		{
			try
			{
				DocumentFile _DocumentFile = GetDocumentFileById(DocumentFile.Id);
				if (_DocumentFile != null)
				{
					var context = new AppDBContext();
					context.Entry<DocumentFile>(DocumentFile).State = EntityState.Modified;
					context.SaveChanges();
					return DocumentFile;
				}
				else
				{
					throw new Exception("The DocumentFile is not exist.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public DocumentFile DeleteDocumentFile(DocumentFile DocumentFile)
		{
			try
			{
				DocumentFile _DocumentFile = GetDocumentFileById(DocumentFile.Id);
				if (_DocumentFile != null)
				{
					var context = new AppDBContext();
					context.DocumentFiles.Remove(_DocumentFile);
					context.SaveChanges();
					return _DocumentFile;
				}
				else
				{
					throw new Exception("The DocumentFile is not exist.");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<DocumentFile> GetDocumentFiles()
		{
			List<DocumentFile> DocumentFiles;
			try
			{
				var context = new AppDBContext();
				DocumentFiles = context.DocumentFiles.ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return DocumentFiles;
		}

		public DocumentFile GetDocumentFileById(int? id)
		{
			DocumentFile DocumentFile;
			try
			{
				var _dbContext = new AppDBContext();
				DocumentFile = _dbContext.DocumentFiles
					.FirstOrDefault(a => a.Id == id);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return DocumentFile;
		}

		public List<DocumentFile> GetDocumentFilesByDocumentId(int? documentId)
		{
			List<DocumentFile> files;
			try
			{
				var _dbContext = new AppDBContext();
				files = _dbContext.DocumentFiles.Where(c => c.DocumentationId == documentId).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return files;
		}
	}
}