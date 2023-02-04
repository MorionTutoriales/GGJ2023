using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreadorAmbiente : MonoBehaviour
{
    public int      alto = 512;
    public int      ancho = 512;
    public int      nodos;
    public Vector2  radios;

	public Vector2	offset;
	public float	tamaño;
	[Range(0,20)]
	public float	sensibilidad;

    public Texture2D    imagenBase;
    public RawImage     rawImage;

	public static CreadorAmbiente singleton;

	public List<Vector2> listaCentros;

	private void Awake()
	{
		singleton = this;
	}

	void Start()
    {
        imagenBase = new Texture2D(alto, ancho);
        GenerarMapa();
        rawImage.texture = imagenBase;
		
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GenerarMapa();
		}
	}

	public void GenerarMapa()
	{
        Color nada = new Color(1, 0, 0, 1);
		listaCentros = new List<Vector2>();

		////// vacío todos los espacios
		for (int i = 0; i < alto; i++)
		{
			for (int j = 0; j < ancho; j++)
			{
                imagenBase.SetPixel(i, j, nada);
			}
		}

		////// Creo mis islas desconectadas
		for (int i = 0; i < nodos; i++)
		{
			float rad = Random.Range(radios.x, radios.y);
            Vector2 centro = new Vector2(Mathf.Round(Random.Range(rad, alto - rad)), Mathf.Round(Random.Range(rad, ancho - rad)));
			listaCentros.Add(centro);
			Color c = new Color(0,0,0,1);
			for (int x = 0; x < alto; x++)
			{
				for (int y = 0; y < ancho; y++)
				{
					if (Vector2.Distance(centro, new Vector2(x,y)) < (rad + sensibilidad * Mathf.PerlinNoise(offset.x + x * (tamaño / 100f), offset.y + y * (tamaño / 100f))))
					{
						imagenBase.SetPixel(x, y, c);
					}
				}
			}
		}

		////// Conecto las islas
		for (int j = 0; j < listaCentros.Count-1; j++)
		{
			for (int i = 1; i < listaCentros.Count - 1; i++)
			{
				if (Vector2.Distance(listaCentros[0], listaCentros[i]) > Vector2.Distance(listaCentros[0], listaCentros[i + 1]))
				{
					Vector2 pos = new Vector2(listaCentros[i].x, listaCentros[i].y);
					listaCentros[i] = listaCentros[i + 1];
					listaCentros[i + 1] = pos;
				}
			}
		}

		for (int i = 1; i < listaCentros.Count; i++)
		{
			print(Vector2.Distance(listaCentros[0], listaCentros[i]));
		}

        imagenBase.Apply();
	}
}
