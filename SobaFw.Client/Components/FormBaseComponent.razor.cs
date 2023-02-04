//namespace SobaFw.Client.Components;

//public partial class FormBaseComponent<TEntity>
//    where TEntity : BaseEntity
//{
//    [Parameter]
//    public TEntity Entity { get; set; }
//    [Parameter]
//    public string FormTitle { get; set; }
//    [Parameter]
//    public EventCallback<TEntity> HandleValidSubmit { get; set; }
//    [Parameter]
//    public EventCallback OnCancel { get; set; }
//    [Parameter]
//    public RenderFragment ChildContent { get; set; }
//    [Parameter]
//    public RenderFragment FooterContent { get; set; }
//    [Parameter]
//    public SystemFeatureType SystemFeature { get; set; }

//    public override Task SetParametersAsync(ParameterView parameters)
//    {
//        if(Entity is null)
//            Entity = Activator.CreateInstance<TEntity>();

//        return base.SetParametersAsync(parameters);
//    }
//}