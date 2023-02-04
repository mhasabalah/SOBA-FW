namespace SobaFw.Client;
public class AppObserver
{
    public Action<List<Guid>>? OnSelectedNodesChanged { get; set; }
    public Action? OnSelectedNodeChanged { get; set; }
    public void SelectedNodesHasChanged(List<Guid> selectedNodes)
    {
        Action<List<Guid>>? selectedNodesChanged = OnSelectedNodesChanged;
        if (selectedNodesChanged == null)
            return;
        selectedNodesChanged(selectedNodes);
    }
    public void SelectedNodeHasChanged() => OnSelectedNodeChanged?.Invoke();
}