using UnityEngine;
using UI = UnityEngine.UI;
using MediaPipe.Selfie;

public sealed class WebcamTest : MonoBehaviour
{
    enum OutputMode { Source, Mask, StaticBG, DynamicBG }

    [SerializeField] WebcamInput _webcam = null;
    [SerializeField] OutputMode _outputMode = OutputMode.StaticBG;
    [SerializeField] Texture2D _bgImage = null;
    [SerializeField] UI.RawImage _outputUI = null;
    [SerializeField] ResourceSet _resources = null;
    [SerializeField] Shader _shader = null;

    SegmentationFilter _filter;
    RenderTexture _composited;
    Material _material;

    void Start()
    {
        _filter = new SegmentationFilter(_resources);
        _composited = new RenderTexture(1920, 1080, 0);
        _material = new Material(_shader);
    }

    void OnDestroy()
    {
        _filter.Dispose();
        Destroy(_composited);
        Destroy(_material);
    }

    void Update()
    {
        _filter.ProcessImage(_webcam.Texture);

        _material.SetTexture("_SourceTexture", _webcam.Texture);
        _material.SetTexture("_MaskTexture", _filter.MaskTexture);
        _material.SetTexture("_BGTexture", _bgImage);
        Graphics.Blit(null, _composited, _material, (int)_outputMode);

        _outputUI.texture = _composited;
    }
}
