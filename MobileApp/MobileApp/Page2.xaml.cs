using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0)
            {
                behaviour.Text = "Podczas powodzi zastosuj się do poleceń prowadzących akcję ratunkową. Nie chodź po obszarach zalanych, jeżeli woda przemieszcza się szybko. Fala o głębokości kilkunastu centymetrów może przewrócić dorosłego człowieka. Jeżeli musisz przekroczyć zalany obszar, wybierz miejsce bez silnego nurtu.";

            }
            else if (selectedIndex == 1)
            {
                behaviour.Text = "W przypadku powstania pożaru wszyscy zobowiązani są podjąć działania w celu jego likwidacji: zaalarmować niezwłocznie, przy użyciu wszystkich dostępnych środków osoby będące w strefie zagrożenia, wezwać straż pożarną.";

            }
            else if (selectedIndex == 2)
            {
                behaviour.Text = "Należy starać się jak najszybciej oddalić z miejsca zdarzenia. Nie ma znaczenia, czy ktokolwiek inny robi to samo, my powinniśmy uciekać. Zostawiamy wszystko, co mamy przy sobie. Na ile to możliwe, staramy się nie biec na oślep, ale mieć jakiś plan, ustaloną drogę ucieczki.";
            }
            else if (selectedIndex == 3)
            {
                behaviour.Text = "W przypadku alarmu bombowego należy natychmiast opuścić miejsce zagrożone i udać się w bezpieczne miejsce z dala od potencjalnego źródła zagrożenia. Należy również bezwzględnie słuchać informacji i zaleceń przekazywanych przez lokalne władze lub służby ratunkowe.";

            }
            else if (selectedIndex == 4)
            {
                behaviour.Text = "Najpierw upewnij się, że przebywasz w bezpiecznym miejscu i uspokój osoby, które są obok ciebie. Szczególnie zadbaj o dzieci i czworonogi. Znajdź schronienia z dala od okien, witryn, przeszklonych drzwi czy luster. Podczas silnych wstrząsów szklane tafle mogą zacząć pękać, a ich odłamki powodować obrażenia.";
            }

        }

    }
}