using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {

    // ==================================================================
    // Elementos de Interface:
    // ==================================================================

    [SerializeField] private Text layoutTitle;
    [SerializeField] private Text layoutDate;
    [SerializeField] private RawImage layoutScale;


    // ==================================================================
    // Informações de Layout (Conteúdo e Data):
    // ==================================================================

    // Nome da informação climática:
    private string[] layoutName = { "No Data Assigned",
                                    "Wind Speed",
                                    "Wind Power Density",
                                    "Temperature",
                                    "Relative Humidity",
                                    "Precipitation Accumulation",
                                    "Available Potential Energy",
                                    "Total Precepitable Water",
                                    "Total Cloud Water",
                                    "Mean Sea Level Pressure",
                                    "Misery Index",
                                    "Ocean Currents",
                                    "Ocean Waves",
                                    "Sea Surface Temperature",
                                    "Sea Surface Temperature Anomaly",
                                    "Significant Wave Height",
                                    "Carbon Monoxide Concentration",
                                    "Carbon Dioxide Concentration",
                                    "Sulfur Dioxide Surface Mass"   };

    // Unidade de medida correspondente:
    private string[] layoutMeasure = {  "0",
                                        "0 to 360 km/h",
                                        "0 to 80 kW/m^2",
                                        "-80 to 55 ºC",
                                        "0 to 100 %",
                                        "0 to 150 mm",
                                        "0 to 5000 J/kg",
                                        "0 to 70000 kg/m^2",
                                        "0 to 1 kg/m^2",
                                        "920 to 1050 hPa",
                                        "-37.1 to 67.8 ºC",
                                        "0 to 1.5 m/s",
                                        "0 to 25 s",
                                        "-3 to 31.5 ºC",
                                        "-6 to 6 ºC",
                                        "0 to 15 m",
                                        "40 to 2500 ppbv",
                                        "360 to 470 ppmv",
                                        "0 to 888 ug/m^3"   };

    // Data em que informação foi obtida:
    private string[] layoutDay = { "02", "03", "04", "05", "06" };
    private string layoutMonth = "07";
    private string layoutYear = "2017";
    private string layoutDateReset = "--/--/----";


    public void UpdateInformationInterface (int indexClimateData, int indexExhibitionDate)
    {
        // Atualiza elementos da interface de acordo com o conteúdo exibido:
        layoutTitle.text = layoutName[indexClimateData] + ": (" + layoutMeasure[indexClimateData] + ")";

        string texturePath = "Images/ColorScales/" + indexClimateData;
        layoutScale.texture = Resources.Load(texturePath, typeof(Texture)) as Texture;

        if (indexClimateData == 0)
        {
            layoutDate.text = layoutDateReset;
        }
        else
        {
            layoutDate.text = layoutDay[indexExhibitionDate-1] + "/" + layoutMonth + "/" + layoutYear;
        }
    }
}
