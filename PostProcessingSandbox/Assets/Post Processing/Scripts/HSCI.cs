using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable, PostProcess(typeof(HSCIRenderer), PostProcessEvent.AfterStack, "Custom/HSCI", true)]
public sealed class HSCI : PostProcessEffectSettings
{
    //public FloatParameter hue = new FloatParameter() { value = 0 };
    //public FloatParameter saturation = new FloatParameter() { value = 1 };
    //public FloatParameter contrast = new FloatParameter() { value = 1 };
    //public FloatParameter invert = new FloatParameter() { value = 0 };

    public FloatParameter power = new FloatParameter() { value = 0 };
    public FloatParameter speed = new FloatParameter() { value = 0 };
    public FloatParameter scale = new FloatParameter() { value = 0 };
}

public sealed class HSCIRenderer : PostProcessEffectRenderer<HSCI>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/HSCI"));
        //sheet.properties.SetFloat("_Hue", settings.hue);
        //sheet.properties.SetFloat("_Saturation", settings.saturation);
        //sheet.properties.SetFloat("_Contrast", settings.contrast);
        //sheet.properties.SetFloat("_Invert", settings.invert);

        sheet.properties.SetFloat("_Power", settings.power);
        sheet.properties.SetFloat("_Speed", settings.speed);
        sheet.properties.SetFloat("_Scale", settings.scale);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
