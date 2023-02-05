using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
	public int		radioCaminos;

	public MeshRenderer mallaPiso;
	private MeshFilter malla;
	private MeshCollider mCollider;
	public float altura;
	public Vector3[] posiciones;
	public float tamañoTerreno = 50;
	float minimo = 10000;
	float maximo = -10000;

	public Texture2D    imagenBase;
    public RawImage     rawImage;

	public static CreadorAmbiente singleton;

	public List<Vector2> listaCentros;

	public GameObject jugador;
	public GameObject boss;
	[Header("Props")]
	public GameObject[] objetosProps;
	List<Vector3> listaBases = new List<Vector3>();
	public float frecuenciaCosas;
	public float escalaCosas;
	public float probabilidadAparecer;
	public int iteracionesCosas;

	private void Awake()
	{
		singleton = this;
		malla = mallaPiso.GetComponent<MeshFilter>();
		mCollider = mallaPiso.GetComponent<MeshCollider>();
	}

	void Start()
    {
        imagenBase = new Texture2D(alto, ancho);
        GenerarMapa();
        rawImage.texture = imagenBase;

		
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
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
		Color c = new Color(0,0,0,1);
		for (int i = 0; i < nodos; i++)
		{
			float rad = Random.Range(radios.x, radios.y);
            Vector2 centro = new Vector2(Mathf.Round(Random.Range(rad, alto - rad)), Mathf.Round(Random.Range(rad, ancho - rad)));
			listaCentros.Add(centro);
			for (int x = 0; x < alto; x++)
			{
				for (int y = 0; y < ancho; y++)
				{
					if (Vector2.Distance(centro, new Vector2(x,y)) < (rad + sensibilidad * Mathf.PerlinNoise(offset.x + x * (tamaño / 100f), offset.y + y * (tamaño / 100f))))
					{
						imagenBase.SetPixel(x, y, c);
						listaBases.Add(new Vector3(((float)x/((float)alto))*50f, 0, ((float)y / ((float)alto)) * 50f));
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

		float m;
		float b;
		for (int i = 1; i < listaCentros.Count; i++)
		{
			//print(Vector2.Distance(listaCentros[0], listaCentros[i]));
			m = (listaCentros[i].y - listaCentros[i - 1].y) / (listaCentros[i].x - listaCentros[i - 1].x);
			b = listaCentros[i].y - m * listaCentros[i].x;

			for (int j = 0; j < Mathf.Abs(listaCentros[i].x -  listaCentros[i-1].x); j++)
			{
				int nx = (int)((listaCentros[i].x < listaCentros[i-1].x)?listaCentros[i].x:listaCentros[i-1].x ) + j;
				int ny = (int)(m * nx + b);
				Dibujarcirculo(radioCaminos, nx, ny, c);
			}
		}

		////// Detectar Bordes
		for (int i = 1; i < ancho; i++)
		{
			for (int j = 1; j < alto; j++)
			{
				Color c1 = imagenBase.GetPixel(i, j+1);
				Color c2 = imagenBase.GetPixel(i, j);
				Color c3 = imagenBase.GetPixel(i, j-1);

				Color c4 = imagenBase.GetPixel(i-1, j);
				Color c5 = imagenBase.GetPixel(i+1, j);


				if (c2 == Color.black && (c3 == Color.red || c1 == Color.red))
				{
					imagenBase.SetPixel(i, j, Color.yellow);
					imagenBase.SetPixel(i, j+1, Color.yellow);
					imagenBase.SetPixel(i, j-1, Color.yellow);
					imagenBase.SetPixel(i-1, j, Color.yellow);
					imagenBase.SetPixel(i-1, j+1, Color.yellow);
					imagenBase.SetPixel(i-1, j-1, Color.yellow);
				}
				if (c2 == Color.black && (c4 == Color.red || c5 == Color.red))
				{
					imagenBase.SetPixel(i, j, Color.yellow);
					imagenBase.SetPixel(i, j + 1, Color.yellow);
					imagenBase.SetPixel(i, j - 1, Color.yellow);
					imagenBase.SetPixel(i - 1, j, Color.yellow);
					imagenBase.SetPixel(i - 1, j + 1, Color.yellow);
					imagenBase.SetPixel(i - 1, j - 1, Color.yellow);
				}

			}
		}

		////// Aplico los cambios en la imagen
		imagenBase.Apply();
		mallaPiso.material.SetTexture("_BaseMap", imagenBase);
		CambiarMalla();

		InstanciarCosas();
	}

	public void Dibujarcirculo(int r, int x, int y, Color c)
	{
		Vector2 p1, p2;
		for (int i = -r; i < r; i++)
		{
			for (int j = -r; j < r; j++)
			{
				p1 = new Vector2(x, y);
				p2 = new Vector2(x + i, y + j);
				if (Vector2.Distance(p1,p2)<=r)
				{
					imagenBase.SetPixel(x + i, y + j, c);
					//listaBases.Add(CalcularPosicion(x, y));
				}
			}
		}
	}

	public void CambiarMalla()
	{
		posiciones = malla.mesh.vertices;
		float nx, nz;
		minimo = 10000;
		maximo = -10000;
		for (int i = 0; i < posiciones.Length; i++)
		{
			if (posiciones[i].x < minimo)
			{
				minimo = posiciones[i].x;
			}
			else if (posiciones[i].x > maximo)
			{
				maximo = posiciones[i].x;
			}
			int ix = ancho-(int)((float)(posiciones[i].x - minimo) / (float)(maximo - minimo)*ancho);
			int iz = ancho-(int)((float)(posiciones[i].z - minimo) / (float)(maximo - minimo)*ancho);
			if (ix >= 0)
			{
				Color c = imagenBase.GetPixel(ix, iz);
				posiciones[i].y = c.r * altura;
			}

		}
		malla.mesh.SetVertices(posiciones);
		malla.mesh.RecalculateNormals();
		mCollider.sharedMesh = malla.mesh;
	}

	public void InstanciarCosas()
	{
		int p = Random.Range(0, listaCentros.Count);
		jugador.transform.position = CalcularPosicion((int)listaCentros[p].x, (int)listaCentros[p].y);

		int p2 = Random.Range(0, listaCentros.Count);
		if (p == p2)
		{
			p2 = (p2 + 1) % listaBases.Count;
		}
		boss.transform.position = CalcularPosicion((int)listaCentros[p2].x, (int)listaCentros[p2].y);

		for (int i = 0; i < iteracionesCosas; i++)
		{
			Vector3 pos = posiciones[Random.Range(0,posiciones.Length)];
			if (pos.y < 0.5f)
			{
				frecuenciaCosas = Random.Range(-100f, 500f);
				float t = Mathf.PerlinNoise(pos.x * escalaCosas / 100f + frecuenciaCosas, pos.y * escalaCosas / 100f + frecuenciaCosas);
				if (t < probabilidadAparecer)
				{
					Instantiate(objetosProps[Random.Range(0, objetosProps.Length)], pos, Quaternion.identity); ;
				}
			}
		}
	}

	public Vector3 CalcularPosicion(int x, int y)
	{
		return new Vector3(
			- (minimo + ((float)x / (float)ancho) * (maximo - minimo)),
			0,
			- (minimo + ((float)y / (float)ancho) * (maximo - minimo))
			);
	}
}
