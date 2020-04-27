package com.example.cipher2.UI

import android.app.Dialog
import android.os.Bundle
import com.example.cipher2.R
import com.example.cipher2.models.TextModel
import kotlinx.android.synthetic.main.result_dialog.*

class SuccessResultDialog(
    private val activity: MainActivity,
    private val textModel: TextModel
) : Dialog(activity) {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.result_dialog)

        sourceTextView.text = textModel.sourceText
        resultTextView.text = textModel.resultText

        okButton.setOnClickListener {
            this.dismiss()
        }

        saveButton.setOnClickListener {
            this.dismiss()
            SaveFileDialog(activity).show()
        }
    }
}
