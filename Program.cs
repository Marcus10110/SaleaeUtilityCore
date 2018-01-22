using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Saleae
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//input file, output file.
			string input_file;
			string output_file;



			if( ParseParameters( args, out input_file, out output_file ) == false )
				return;


			var reader = new System.IO.StreamReader( System.IO.File.OpenRead(  input_file ) );
			int row = 0;

			var writer = System.IO.File.OpenWrite( output_file );

			while( reader.EndOfStream == false )
			{
				string line = reader.ReadLine();
				if( row++ == 0 )
					continue; //skip header
				var elements = line.Split( ',' ).Select( x => x.Trim() ).ToList();

				if( elements.Count() != 4 )
				{
					Console.WriteLine( "error parsing line " + row );
					return;
				}

				string hex_string = elements[ 1 ];
				if( hex_string.StartsWith( "0x" ) == false )
				{
					Console.WriteLine( "error: expected HEX formatting." );
					return;
				}
				int value = 0;
                try
				{
					value = Convert.ToInt32( hex_string.Substring( 2 ), 16 );
				}
				catch( Exception /* ex */ )
				{
					Console.WriteLine( "error: parse failure on row " + row );
					Console.WriteLine( "unknown value: " + hex_string );
					return;
				}

				if( value > 255 || value < 0 )
				{
					Console.WriteLine( "error: only 8 bit numbers are supported. value " + value + " out of range" );
					return;
				}

				Byte value_byte = ( byte ) value;

				writer.WriteByte( value_byte );

			}

			writer.Close();

			Console.WriteLine( "Finished! " + row + " rows written" );

		}

		private static bool ParseParameters( String[] parameters, out string input_file, out string output_file )
		{
			input_file = null;
			output_file = null;

			//--help -help /help /? -?

			string general_help_str = "Saleae Serial Export txt to bin converter\n";
			general_help_str += "usage: SaleaeUtility.exe [input_file] [output_file]\n";
			general_help_str += "dotnet usage: dotnet run -- [input_file] [output_file]\n";

			var help_parameters = new string[] { "--help", "-help", "-h", "/?", "/help", "-?" };

			if( parameters.Any( x => help_parameters.Contains( x.ToLower() ) ) || parameters.Count() == 0 )
			{
				Console.WriteLine( general_help_str );
				return false;
			}

			if( parameters.Count() != 2 )
			{
				Console.WriteLine( "expected 2 parameters" );
				return false;
            }

			input_file = parameters[ 0 ];

			output_file = parameters[ 1 ];

			if( System.IO.File.Exists( input_file ) == false )
			{
				Console.WriteLine( "input file " + input_file + " does not exist" );
				return false;
			}

			string output_folder = new System.IO.FileInfo( output_file ).Directory.FullName;

			if( System.IO.Directory.Exists( output_folder ) == false )
			{
				Console.WriteLine( "output file directory " + output_folder + " does not exist" );
				return false;
			}

			return true;

		}
    }
}
