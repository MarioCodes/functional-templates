package es.codes.mario.template.activity

import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.databinding.DataBindingUtil
import androidx.preference.PreferenceFragmentCompat
import es.codes.mario.template.R
import es.codes.mario.template.databinding.SettingsActivityBinding
import es.codes.mario.template.di.DaggerAppComponent
import es.codes.mario.template.viewmodel.EntryViewModel
import javax.inject.Inject

class SettingsActivity : AppCompatActivity() {

    private lateinit var binding: SettingsActivityBinding

    @Inject
    lateinit var entryViewModel: EntryViewModel

    init {
        DaggerAppComponent.create().inject(this)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = DataBindingUtil.setContentView(this, R.layout.settings_activity)
        supportFragmentManager
            .beginTransaction()
            .replace(
                R.id.settings,
                SettingsFragment()
            )
            .commit()
        supportActionBar?.setDisplayHomeAsUpEnabled(true)
    }

    class SettingsFragment : PreferenceFragmentCompat() {
        override fun onCreatePreferences(savedInstanceState: Bundle?, rootKey: String?) {
            setPreferencesFromResource(R.xml.preferences_main, rootKey)
        }
    }

    fun exportFile(view: View) {
        Toast.makeText(view.context, "Export button pressed", Toast.LENGTH_LONG).show()
    }
}