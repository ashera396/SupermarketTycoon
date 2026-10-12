using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CustomerAI : MonoBehaviour
{
    public Transform[] urunNoktalar;     // Buzdolabý, raf vs. tüm noktalar buraya
    public Transform cashierPoint;
    public Transform exitPoint;
    public CustomerSpawner spawner;

    private enum CustomerState
    {
        GoToProduct,
        TakingProduct,
        GoToCashier,
        Paying,
        GoToExit
    }

    private CustomerState currentState;
    private NavMeshAgent agent;
    private bool isWaiting = false;
    private StockManager hedefStok;
    private Transform hedefNokta;       // Seçilen ürün noktasý

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Rastgele bir ürün noktasý seç
        hedefNokta = urunNoktalar[Random.Range(0, urunNoktalar.Length)];
        hedefStok = hedefNokta.GetComponentInParent<StockManager>();

        currentState = CustomerState.GoToProduct;
        agent.SetDestination(hedefNokta.position);
    }

    void Update()
    {
        if (isWaiting) return;

        switch (currentState)
        {
            case CustomerState.GoToProduct:
                if (HedefeBulundu())
                {
                    StartCoroutine(Bekle(2f, CustomerState.TakingProduct));
                }
                break;

            case CustomerState.TakingProduct:
                if (hedefStok != null && hedefStok.UrunAl())
                {
                    currentState = CustomerState.GoToCashier;
                    agent.SetDestination(cashierPoint.position);
                }
                else
                {
                    RatingManager.Instance.MusteriMutsuz();
                    currentState = CustomerState.GoToExit;
                    agent.SetDestination(exitPoint.position);
                }
                break;

            case CustomerState.GoToCashier:
                if (HedefeBulundu())
                {
                    spawner.KasayaGeldi();
                    MoneyManager.Instance.ParaKazan(hedefStok != null ? hedefStok.fiyat : 5);
                    RatingManager.Instance.MusteriMemnun();
                    StartCoroutine(Bekle(2f, CustomerState.Paying));
                }
                break;

            case CustomerState.Paying:
                spawner.KasadanAyrildi();
                currentState = CustomerState.GoToExit;
                agent.SetDestination(exitPoint.position);
                break;

            case CustomerState.GoToExit:
                if (HedefeBulundu())
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private bool HedefeBulundu()
    {
        if (agent.pathPending) return false;
        return agent.remainingDistance <= agent.stoppingDistance + 0.1f;
    }

    private IEnumerator Bekle(float sure, CustomerState sonrakiDurum)
    {
        isWaiting = true;
        agent.ResetPath();
        yield return new WaitForSeconds(sure);
        currentState = sonrakiDurum;
        isWaiting = false;
    }
}