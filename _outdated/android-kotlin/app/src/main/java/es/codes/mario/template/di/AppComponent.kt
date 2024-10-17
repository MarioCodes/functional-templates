package es.codes.mario.template.di

import dagger.Component
import es.codes.mario.template.activity.SettingsActivity
import es.codes.mario.template.charts.AndroidPlotChart
import es.codes.mario.template.fragment.AddEntryFragment
import es.codes.mario.template.fragment.DeleteEntryFragment
import es.codes.mario.template.fragment.MainFragment
import es.codes.mario.template.viewmodel.EntryViewModel

@Component(modules = [AppModule::class])
interface AppComponent {

    // classes that need injections go here

    fun inject(chart: AndroidPlotChart)

    fun inject(settingsActivity: SettingsActivity)

    fun inject(addEntryFragment: AddEntryFragment)

    fun inject(deleteEntryFragment: DeleteEntryFragment)

    fun inject(entryViewModel: EntryViewModel)

    fun inject(mainFragment: MainFragment)

}