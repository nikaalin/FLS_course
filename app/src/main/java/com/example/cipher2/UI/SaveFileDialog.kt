package com.example.cipher2.UI

import android.app.Dialog
import android.os.Bundle
import com.example.cipher2.R
import com.example.cipher2.exceptions.ApplicationException
import com.example.cipher2.exceptions.FileAlreadyExists
import kotlinx.android.synthetic.main.save_dialog.*
import java.io.File


class SaveFileDialog(
    private val activity: MainActivity
) : Dialog(activity) {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.save_dialog)


        saveNamedFileButton.setOnClickListener {
            try {
                activity.fileNameToSave = fileNameEditText.text.toString()
                saveDocx()
                this.dismiss()
            }catch(e:ApplicationException){
                e.toast(activity)
            }
        }
    }

    private fun saveDocx() {
        val name = activity.fileNameToSave
        val type = activity.fileTypeToSave
        val path = activity.getExternalFilesDir(type)?.path

        val file = File("$path/$name.$type")
        if (!file.exists())
            file.writeText(activity.model.resultText)
        else throw FileAlreadyExists()
    }

}

