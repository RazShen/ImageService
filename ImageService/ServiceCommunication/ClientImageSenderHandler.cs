using ImageServiceTools.Controller;
using ImageServiceTools.Logging;
using ImageServiceTools.ServiceCommunication;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.ServiceCommunication
	{
	class ClientImageSenderHandler: IClientHandler
		{
		IImageController ImageController { get; set; }
		ILoggingService Logging { get; set; }
		/// <summary>
		/// Constructor for the image client.
		/// </summary>
		/// <param name="imageController"></param>
		/// <param name="logging"></param>
		public ClientImageSenderHandler(IImageController imageController, ILoggingService logging)
			{
			this.ImageController = imageController;
			this.Logging = logging;

			}
		/// <summary>
		/// This function handlers a single client connection for transferring image, first: name(string as bits) second: image(by bits).
		/// </summary>
		/// <param name="client"></param>
		/// <param name="clients"></param>
		public void HandleClient(TcpClient client, List<TcpClient> clients)
			{
			try
				{

				new Task(() =>
				{
					try
						{
						Logging.Log("Handling client byte transfer", MessageTypeEnum.INFO);

						NetworkStream stream = client.GetStream();
						Byte[] data = new Byte[6790];
						Byte[] HelpingName = new Byte[1];
						Byte[] indicator = new byte[1];
						List<Byte> finalbytes = new List<byte>();
						String nameImage;
						String fixedName;
						List<Byte> finalName = new List<byte>();
						Byte[] helpingBytes;
						int i =0, nameCounter = 0;
						// Read the name
						do
							{
							nameCounter = stream.Read(HelpingName, 0, 1);
							finalName.Add(HelpingName[0]);
							} while (stream.DataAvailable);

						nameImage = System.Text.Encoding.UTF8.GetString(finalName.ToArray());
						int jpgIndex = nameImage.LastIndexOf(".jpg");
						fixedName = nameImage.Substring(0, jpgIndex);
						indicator[0] = 1;
						// Send to the client approve that we read the name and now we are ready for the image bits.
						stream.Write(indicator, 0, 1);
						// Get the image by bits using a buffer and a array to hold the buffer total data 
						//(iter after iter we put the buffer in the array)
						do
							{
							i = stream.Read(data, 0, data.Length);
							helpingBytes = new byte[i];
							for (int n = 0; n < i; n++)
								{
								helpingBytes[n] = data[n];
								finalbytes.Add(helpingBytes[n]);

								}
							// sleep is necessary here since the stream buffer needs some time to fill up from the client
							System.Threading.Thread.Sleep(200);
							} while (stream.DataAvailable || i == data.Length);
						// Put the image in the first handler.
						File.WriteAllBytes(ImageController.ImageServer.pathOfDefaultHandlerForTCP+ @"\"+fixedName+".jpg", finalbytes.ToArray());
						System.Threading.Thread.Sleep(500);
						client.Close();
						}
					catch (Exception ex)
						{
						Logging.Log(ex.ToString(), MessageTypeEnum.ERROR);
						client.Close();
						}

				}).Start();
				}
			catch (Exception ex)
				{
				Logging.Log(ex.ToString(), MessageTypeEnum.ERROR);

				}


			}


		}
	}
