package es.codes.mario.template.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.databinding.ViewDataBinding
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import es.codes.mario.template.R
import es.codes.mario.template.di.DaggerAppComponent
import es.codes.mario.template.viewmodel.EntryViewModel
import es.codes.mario.template.utils.DateUtils
import kotlinx.android.synthetic.main.add_entry_example.view.*
import java.util.*
import javax.inject.Inject

class AddEntryFragment : Fragment() {

    private lateinit var binding: ViewDataBinding

    private lateinit var entryViewModel: EntryViewModel

    @Inject
    lateinit var dateUtils: DateUtils

    init {
        DaggerAppComponent.create().inject(this)
    }

    companion object {
        var selectedDate: Long = 0L
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = DataBindingUtil.inflate<ViewDataBinding>(inflater, R.layout.add_entry_example, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.root.add_entry_button_cancel.setOnClickListener {
            findNavController().navigate(R.id.action_addEntryFragment_to_FirstFragment)
        }

        binding.root.add_entry_button_save.setOnClickListener {
            // do something here
        }

        val dateCalendarView = binding.root.add_date_calendarView
        dateCalendarView.setOnDateChangeListener { localView, year, month, dayOfMonth ->
            val calendar = Calendar.getInstance()
            calendar.set(Calendar.YEAR, year)
            calendar.set(Calendar.MONTH, month)
            calendar.set(Calendar.DAY_OF_MONTH, dayOfMonth)
            selectedDate = calendar.timeInMillis
        }

        entryViewModel = ViewModelProviders.of(requireActivity()).get(EntryViewModel::class.java)
        selectedDate = dateCalendarView.date
    }

}